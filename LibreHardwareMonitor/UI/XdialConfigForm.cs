using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibreHardwareMonitor.Utilities;

namespace LibreHardwareMonitor.UI
{
    public partial class XdialConfigForm : Form
    {
        private readonly MainForm _parent;
        private readonly PersistentSettings _settings;
        public XdialConfigForm(MainForm m, PersistentSettings settings)
        {
            InitializeComponent();
            _parent = m;
            _settings = settings;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            _settings.SetValue("xdial.ip", ipTextBox.Text);
            _settings.SetValue("xdial.cpu.load", cpuLoadTextBox.Text);
            // _settings.SetValue("xdial.cpu.temp", cpuTempTextBox.Text);
            // _settings.SetValue("xdial.gpu.temp", gpuTempTextBox.Text);
            _settings.SetValue("xdial.gpu.load", gpuLoadTextBox.Text);
            _settings.SetValue("xdial.network.upload", uploadTextBox.Text);
            _settings.SetValue("xdial.network.download", downloadTextBox.Text);
            // _settings.SetValue("xdial.disk.load", diskLoadTextBox.Text);
            _settings.SetValue("xdial.ram.load", ramLoadTextBox.Text);
            Close();
        }

        private void XdialConfigForm_Load(object sender, EventArgs e)
        {
            ipTextBox.Text = _settings.GetValue("xdial.ip", "");
            cpuLoadTextBox.Text = _settings.GetValue("xdial.cpu.load", "");
            // cpuTempTextBox.Text = _settings.GetValue("xdial.cpu.temp", "");
            gpuLoadTextBox.Text = _settings.GetValue("xdial.gpu.load", "");
            // gpuTempTextBox.Text = _settings.GetValue("xdial.gpu.temp", "");
            downloadTextBox.Text = _settings.GetValue("xdial.network.download", "");
            uploadTextBox.Text = _settings.GetValue("xdial.network.upload", "");
            ramLoadTextBox.Text = _settings.GetValue("xdial.ram.load", "");
            // diskLoadTextBox.Text = _settings.GetValue("xdial.disk.load", "");
        }

        
    }
}
