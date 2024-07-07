using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using NAudio.CoreAudioApi;
using System.Windows.Forms;
using LibreHardwareMonitor.UI;

namespace LibreHardwareMonitor.Utilities;

public class XdialException: Exception
{
    public XdialException(): base() {}
    public XdialException(string msg): base(msg) {}
    public XdialException(string msg, Exception innerEx): base(msg, innerEx) {}
}
public class XdialPerfmonClient
{
    private ClientWebSocket clientWebSocket;
    private CancellationTokenSource cancellationTokenSource;
    private MMDevice audioDevice;
    private Dictionary<string, SensorNode> sensors;
    private byte[] sendBuffer = new byte[8+8*2];
    
    public XdialPerfmonClient()
    {
        sensors = new Dictionary<string, SensorNode>();
        audioDevice = new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);        
    }
    ~XdialPerfmonClient()
    {
        StopWebSocket().Wait();
    }
    public string ServerIP
    {
        get;set;
    }
    public int Interval
    {
        get;set;
    }
    public int AudioVolume
    {
        get{
            return audioDevice.AudioEndpointVolume.Mute ? 0 : Convert.ToInt32(audioDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
        }
        set {
            if (value<0 || value>100)
                return;
            audioDevice.AudioEndpointVolume.MasterVolumeLevelScalar = value/100.0f;
            if (value >0)
                audioDevice.AudioEndpointVolume.Mute =false;
        }
    }
    public void ResetSensors(Node root, PersistentSettings settings)
    {
        sensors.Clear();
        foreach (string s in
            "cpu.load/gpu.load/network.upload/network.download/ram.load"
            .Split('/'))
        {
            sensors.Add(s, HttpServer.FindSensor(root, settings.GetValue($"xdial.{s}", "")));
        };
    }
    private SensorNode GetSensorNode(string name)
    {
        return sensors.TryGetValue(name, out SensorNode sn)? sn: null;
    }
    private sbyte GetSensorValueInSbyte(string name)
    {
        return (sbyte)Math.Round(GetSensorNode(name)?.Sensor.Value ?? 0);
    }
    private int UpdateSendBuffer()
    {
        sendBuffer[0] = (byte)GetSensorValueInSbyte("cpu.load");
        sendBuffer[1] = (byte)GetSensorValueInSbyte("ram.load");
        sendBuffer[2] = (byte)GetSensorValueInSbyte("gpu.load");
        sendBuffer[3] = (byte)AudioVolume;
        byte[] arr = Encoding.UTF8.GetBytes((GetSensorNode("network.upload")?.Value ?? "0.0 KB/s").Replace(" ", "").Replace("B/s",""));
        Array.Copy(arr, 0, sendBuffer, 8, arr.Length);
        Array.Copy(new byte[8-arr.Length], 0, sendBuffer, 8+arr.Length, 8-arr.Length);
        arr = Encoding.UTF8.GetBytes((GetSensorNode("network.download")?.Value ?? "0.0 KB/s").Replace(" ","").Replace("B/s",""));
        Array.Copy(arr, 0, sendBuffer, 8+8, arr.Length);
        Array.Copy(new byte[8-arr.Length], 0, sendBuffer, 8+8+arr.Length, 8-arr.Length);
        return sendBuffer.Length;
    }
    private int i=0;
    public async Task StartWebSocket()
    {
        if(this.IsRunning)
            return;
            
        cancellationTokenSource = new CancellationTokenSource();
        var ts = cancellationTokenSource;
        while(! ts.Token.IsCancellationRequested)
        {
            try
            {
                using (clientWebSocket = new ClientWebSocket())
                {
                    var ws = clientWebSocket;
                    Uri uri;
                    try
                    {
                        uri = new Uri($"ws://{ServerIP}/perf");
                    }
                    catch(Exception ex)
                    {
                        throw new XdialException("xdial.ex.invalid_uri", ex);
                    }
                    await ws.ConnectAsync(uri, ts.Token);
                    Console.WriteLine("websocket connected");
                    Task<byte[]> recvMsgTask = recvMsg(ws, ts.Token);
                    if(await Task.WhenAny(recvMsgTask, Task.Delay(5000, ts.Token))!= recvMsgTask)
                    {
                        throw new XdialException("xdial.ex.wrong_uri");
                    }
                    Version ver;
                    try
                    {
                        string? verStr = (string)JObject.Parse(Encoding.UTF8.GetString(await recvMsgTask))["version"];
                        ver = new Version(verStr);
                    }
                    catch(Exception ex)
                    {
                        throw new XdialException("xdial.ex.wrong_uri", ex);
                    }
                    if (ver.CompareTo(new Version("1.0.0")) > 0) {
                        throw new XdialException("xdial.ex.version_too_old");
                    }                    
                    await Task.WhenAny(sendTask(ws, ts.Token), recvTask(ws, ts.Token));
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                }//using
            }//try
            catch (Exception ex) when (ex is not XdialException)
            {
                Console.WriteLine($"StartWebSocket err: {ex.Message}");
            }
            try
            {
                await Task.Delay(10_000, ts.Token);
            }
            catch(OperationCanceledException ex)
            {
                Console.WriteLine($"StartWebSocket err: {ex.Message}");
            }
        }
    }

    private async Task sendTask(ClientWebSocket ws, CancellationToken token)
    {
        try
        {
            while(! token.IsCancellationRequested)
            {
                int len = UpdateSendBuffer();
                await ws.SendAsync(new ArraySegment<byte>(sendBuffer,0,len), WebSocketMessageType.Binary, true, token);
                await Task.Delay(Interval);                
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"sendTask err: {ex.Message}");
            throw ex;
        }
    }

    public bool IsRunning
    {
        get{
            return clientWebSocket!=null && (clientWebSocket.State==WebSocketState.Open || clientWebSocket.State==WebSocketState.Connecting);
        }
    }

    public async Task StopWebSocket()
    {

        try
        {
            if (IsRunning)
                // 不知道为什么用 await 会无限阻塞，导致下一句cancel 不能执行
                // await
                clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by user", CancellationToken.None);
        }
        catch (Exception ex)
        { }
        cancellationTokenSource?.Cancel();

    }

    private async Task recvTask(ClientWebSocket ws, CancellationToken token)
    {
        try
        {
            while(! token.IsCancellationRequested)
            {
                byte[] bs = await recvMsg(ws, token);
                if(bs != null)
                    AudioVolume += BitConverter.ToInt32(bs, 0);;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"recvTask err: {ex.Message}");
            throw ex;
        }
    }
    private async Task<byte[]> recvMsg(ClientWebSocket ws, CancellationToken token)
    {
        byte[] buffer = new byte[128];
        WebSocketReceiveResult result = null;
        int ofs = 0;
        do
        {
            result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer, ofs, buffer.Length-ofs), token);
            ofs += result.Count;            
        } while(! result.EndOfMessage);
        if (result.MessageType == WebSocketMessageType.Text)
        {
            buffer[result.Count]=0;
            return buffer;
        }
        else if (result.MessageType == WebSocketMessageType.Binary)
        {
            return buffer;
        }
        else if (result.MessageType == WebSocketMessageType.Close)
        {
            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", token);
        }
        return null;
        
    }

    // private async Task<string> recvMsg(ClientWebSocket ws, CancellationToken token)
    // {
    //     byte[] buffer = new byte[64];
    //     WebSocketReceiveResult result = null;
    //     StringBuilder sb = new StringBuilder();
    //     do 
    //     {
    //         result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), token);
    //         if (result.MessageType == WebSocketMessageType.Text)
    //         {
    //             sb.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
    //         }
    //         else if (result.MessageType == WebSocketMessageType.Close)
    //         {
    //             await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "", token);
    //             break;
    //         }
    //     } while (! result.EndOfMessage);
    //     return sb.ToString();
    // }
    
    
}
