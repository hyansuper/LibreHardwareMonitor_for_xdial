namespace LibreHardwareMonitor.UI
{
    partial class XdialConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XdialConfigForm));
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.ipLabel = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.downloadTextBox = new System.Windows.Forms.TextBox();
            this.downloadLabel = new System.Windows.Forms.Label();
            this.uploadTextBox = new System.Windows.Forms.TextBox();
            this.uploadLabel = new System.Windows.Forms.Label();
            this.ramLoadTextBox = new System.Windows.Forms.TextBox();
            this.ramLoadLabel = new System.Windows.Forms.Label();
            this.cpuLoadTextBox = new System.Windows.Forms.TextBox();
            this.cpuLoadLabel = new System.Windows.Forms.Label();
            this.gpuLoadTextBox = new System.Windows.Forms.TextBox();
            this.gpuLoadLabel = new System.Windows.Forms.Label();
            this.sensorIdGroupBox = new System.Windows.Forms.GroupBox();
            this.sensorIdGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ipTextBox
            // 
            resources.ApplyResources(this.ipTextBox, "ipTextBox");
            this.ipTextBox.Name = "ipTextBox";
            // 
            // ipLabel
            // 
            resources.ApplyResources(this.ipLabel, "ipLabel");
            this.ipLabel.Name = "ipLabel";
            // 
            // cancelBtn
            // 
            resources.ApplyResources(this.cancelBtn, "cancelBtn");
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // okBtn
            // 
            resources.ApplyResources(this.okBtn, "okBtn");
            this.okBtn.Name = "okBtn";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // downloadTextBox
            // 
            resources.ApplyResources(this.downloadTextBox, "downloadTextBox");
            this.downloadTextBox.Name = "downloadTextBox";
            // 
            // downloadLabel
            // 
            resources.ApplyResources(this.downloadLabel, "downloadLabel");
            this.downloadLabel.Name = "downloadLabel";
            // 
            // uploadTextBox
            // 
            resources.ApplyResources(this.uploadTextBox, "uploadTextBox");
            this.uploadTextBox.Name = "uploadTextBox";
            // 
            // uploadLabel
            // 
            resources.ApplyResources(this.uploadLabel, "uploadLabel");
            this.uploadLabel.Name = "uploadLabel";
            // 
            // ramLoadTextBox
            // 
            resources.ApplyResources(this.ramLoadTextBox, "ramLoadTextBox");
            this.ramLoadTextBox.Name = "ramLoadTextBox";
            // 
            // ramLoadLabel
            // 
            resources.ApplyResources(this.ramLoadLabel, "ramLoadLabel");
            this.ramLoadLabel.Name = "ramLoadLabel";
            // 
            // cpuLoadTextBox
            // 
            resources.ApplyResources(this.cpuLoadTextBox, "cpuLoadTextBox");
            this.cpuLoadTextBox.Name = "cpuLoadTextBox";
            // 
            // cpuLoadLabel
            // 
            resources.ApplyResources(this.cpuLoadLabel, "cpuLoadLabel");
            this.cpuLoadLabel.Name = "cpuLoadLabel";
            // 
            // gpuLoadTextBox
            // 
            resources.ApplyResources(this.gpuLoadTextBox, "gpuLoadTextBox");
            this.gpuLoadTextBox.Name = "gpuLoadTextBox";
            // 
            // gpuLoadLabel
            // 
            resources.ApplyResources(this.gpuLoadLabel, "gpuLoadLabel");
            this.gpuLoadLabel.Name = "gpuLoadLabel";
            // 
            // sensorIdGroupBox
            // 
            resources.ApplyResources(this.sensorIdGroupBox, "sensorIdGroupBox");
            this.sensorIdGroupBox.Controls.Add(this.cpuLoadTextBox);
            this.sensorIdGroupBox.Controls.Add(this.cpuLoadLabel);
            this.sensorIdGroupBox.Controls.Add(this.gpuLoadTextBox);
            this.sensorIdGroupBox.Controls.Add(this.gpuLoadLabel);
            this.sensorIdGroupBox.Controls.Add(this.ramLoadTextBox);
            this.sensorIdGroupBox.Controls.Add(this.ramLoadLabel);
            this.sensorIdGroupBox.Controls.Add(this.uploadTextBox);
            this.sensorIdGroupBox.Controls.Add(this.uploadLabel);
            this.sensorIdGroupBox.Controls.Add(this.downloadTextBox);
            this.sensorIdGroupBox.Controls.Add(this.downloadLabel);
            this.sensorIdGroupBox.Name = "sensorIdGroupBox";
            this.sensorIdGroupBox.TabStop = false;
            // 
            // XdialConfigForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sensorIdGroupBox);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.ipTextBox);
            this.Controls.Add(this.ipLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XdialConfigForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.XdialConfigForm_Load);
            this.sensorIdGroupBox.ResumeLayout(false);
            this.sensorIdGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.TextBox cpuLoadTextBox;
        private System.Windows.Forms.Label cpuLoadLabel;
        private System.Windows.Forms.TextBox gpuLoadTextBox;
        private System.Windows.Forms.Label gpuLoadLabel;
        private System.Windows.Forms.TextBox ramLoadTextBox;
        private System.Windows.Forms.Label ramLoadLabel;
        private System.Windows.Forms.TextBox downloadTextBox;
        private System.Windows.Forms.Label downloadLabel;
        private System.Windows.Forms.TextBox uploadTextBox;
        private System.Windows.Forms.Label uploadLabel;
        private System.Windows.Forms.GroupBox sensorIdGroupBox;
    }
}
