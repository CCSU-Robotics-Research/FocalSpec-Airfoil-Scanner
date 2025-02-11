namespace Photogrametry
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_RapContinue = new System.Windows.Forms.Button();
            this.btn_StopRap = new System.Windows.Forms.Button();
            this.btn_StartRAP = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbl_SelectFolder = new System.Windows.Forms.Label();
            this.btn_SelectFolder = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.CameraFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Frames = new System.Windows.Forms.NumericUpDown();
            this.btn_Capture = new System.Windows.Forms.Button();
            this.lbl_Interval = new System.Windows.Forms.Label();
            this.Interval = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_ssh_IpAddress = new System.Windows.Forms.Label();
            this.ssh_IpAddress = new System.Windows.Forms.TextBox();
            this.lbl_WSLStatus = new System.Windows.Forms.Label();
            this.SubRunning = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.hardwareID = new System.Windows.Forms.TextBox();
            this.btn_WSLStart = new System.Windows.Forms.Button();
            this.btn_WSLStop = new System.Windows.Forms.Button();
            this.btn_ConnectCTRL = new System.Windows.Forms.Button();
            this.listView_Controllers = new System.Windows.Forms.ListView();
            this.IPAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ControllerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_ScanCTRLS = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_SaveLog = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.cycleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.executionTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.motionDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.motionPointerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.programPointerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remainingCyclesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraHardwareIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Frames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Interval)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.btn_RapContinue);
            this.groupBox1.Controls.Add(this.btn_StopRap);
            this.groupBox1.Controls.Add(this.btn_StartRAP);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btn_ConnectCTRL);
            this.groupBox1.Controls.Add(this.listView_Controllers);
            this.groupBox1.Controls.Add(this.btn_ScanCTRLS);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btn_SaveLog);
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Location = new System.Drawing.Point(1, 39);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1361, 697);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "f";
            // 
            // btn_RapContinue
            // 
            this.btn_RapContinue.Location = new System.Drawing.Point(253, 135);
            this.btn_RapContinue.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_RapContinue.Name = "btn_RapContinue";
            this.btn_RapContinue.Size = new System.Drawing.Size(75, 23);
            this.btn_RapContinue.TabIndex = 45;
            this.btn_RapContinue.Text = "Continue";
            this.btn_RapContinue.UseVisualStyleBackColor = true;
            this.btn_RapContinue.Click += new System.EventHandler(this.btn_RapContinue_Click);
            // 
            // btn_StopRap
            // 
            this.btn_StopRap.Location = new System.Drawing.Point(140, 135);
            this.btn_StopRap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_StopRap.Name = "btn_StopRap";
            this.btn_StopRap.Size = new System.Drawing.Size(107, 23);
            this.btn_StopRap.TabIndex = 44;
            this.btn_StopRap.Text = "Stop Rapid";
            this.btn_StopRap.UseVisualStyleBackColor = true;
            this.btn_StopRap.Click += new System.EventHandler(this.btn_StopRapClick_1);
            // 
            // btn_StartRAP
            // 
            this.btn_StartRAP.Location = new System.Drawing.Point(13, 137);
            this.btn_StartRAP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_StartRAP.Name = "btn_StartRAP";
            this.btn_StartRAP.Size = new System.Drawing.Size(120, 23);
            this.btn_StartRAP.TabIndex = 44;
            this.btn_StartRAP.Text = "Start Rapid";
            this.btn_StartRAP.UseVisualStyleBackColor = true;
            this.btn_StartRAP.Click += new System.EventHandler(this.btn_StartRAP_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbl_SelectFolder);
            this.groupBox3.Controls.Add(this.btn_SelectFolder);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.CameraFolder);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.Frames);
            this.groupBox3.Controls.Add(this.btn_Capture);
            this.groupBox3.Controls.Add(this.lbl_Interval);
            this.groupBox3.Controls.Add(this.Interval);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(1079, 338);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(267, 252);
            this.groupBox3.TabIndex = 43;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "GPhoto Controls";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // lbl_SelectFolder
            // 
            this.lbl_SelectFolder.AutoSize = true;
            this.lbl_SelectFolder.Location = new System.Drawing.Point(11, 142);
            this.lbl_SelectFolder.Name = "lbl_SelectFolder";
            this.lbl_SelectFolder.Size = new System.Drawing.Size(123, 16);
            this.lbl_SelectFolder.TabIndex = 49;
            this.lbl_SelectFolder.Text = "Select Local Folder";
            // 
            // btn_SelectFolder
            // 
            this.btn_SelectFolder.Location = new System.Drawing.Point(8, 160);
            this.btn_SelectFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_SelectFolder.Name = "btn_SelectFolder";
            this.btn_SelectFolder.Size = new System.Drawing.Size(157, 23);
            this.btn_SelectFolder.TabIndex = 46;
            this.btn_SelectFolder.Text = "Browse";
            this.btn_SelectFolder.UseVisualStyleBackColor = true;
            this.btn_SelectFolder.Click += new System.EventHandler(this.btn_SelectFolder_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 190);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 16);
            this.label4.TabIndex = 47;
            this.label4.Text = "Camera Image Folder";
            // 
            // CameraFolder
            // 
            this.CameraFolder.Location = new System.Drawing.Point(11, 210);
            this.CameraFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CameraFolder.Name = "CameraFolder";
            this.CameraFolder.Size = new System.Drawing.Size(167, 22);
            this.CameraFolder.TabIndex = 46;
            this.CameraFolder.Text = "/store_00010001/DCIM/100D7500";
            this.CameraFolder.TextChanged += new System.EventHandler(this.CameraFolder_TextChanged_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 16);
            this.label2.TabIndex = 41;
            this.label2.Text = "Frames To Capture";
            // 
            // Frames
            // 
            this.Frames.Location = new System.Drawing.Point(8, 60);
            this.Frames.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Frames.Name = "Frames";
            this.Frames.Size = new System.Drawing.Size(149, 22);
            this.Frames.TabIndex = 37;
            this.Frames.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Frames.ValueChanged += new System.EventHandler(this.Frames_ValueChanged);
            // 
            // btn_Capture
            // 
            this.btn_Capture.Location = new System.Drawing.Point(171, 90);
            this.btn_Capture.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Capture.Name = "btn_Capture";
            this.btn_Capture.Size = new System.Drawing.Size(96, 44);
            this.btn_Capture.TabIndex = 40;
            this.btn_Capture.Text = "Capture Photos";
            this.btn_Capture.UseVisualStyleBackColor = true;
            this.btn_Capture.Click += new System.EventHandler(this.btn_Capture_Click);
            // 
            // lbl_Interval
            // 
            this.lbl_Interval.AutoSize = true;
            this.lbl_Interval.Location = new System.Drawing.Point(8, 90);
            this.lbl_Interval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Interval.Name = "lbl_Interval";
            this.lbl_Interval.Size = new System.Drawing.Size(79, 16);
            this.lbl_Interval.TabIndex = 41;
            this.lbl_Interval.Text = "Interval (ms)";
            // 
            // Interval
            // 
            this.Interval.Location = new System.Drawing.Point(8, 111);
            this.Interval.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Interval.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Interval.Name = "Interval";
            this.Interval.Size = new System.Drawing.Size(149, 22);
            this.Interval.TabIndex = 37;
            this.Interval.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.Interval.ValueChanged += new System.EventHandler(this.Interval_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_ssh_IpAddress);
            this.groupBox2.Controls.Add(this.ssh_IpAddress);
            this.groupBox2.Controls.Add(this.lbl_WSLStatus);
            this.groupBox2.Controls.Add(this.SubRunning);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.hardwareID);
            this.groupBox2.Controls.Add(this.btn_WSLStart);
            this.groupBox2.Controls.Add(this.btn_WSLStop);
            this.groupBox2.Location = new System.Drawing.Point(1079, 23);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(267, 290);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Linux Sub System Controls";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // lbl_ssh_IpAddress
            // 
            this.lbl_ssh_IpAddress.AutoSize = true;
            this.lbl_ssh_IpAddress.Location = new System.Drawing.Point(8, 94);
            this.lbl_ssh_IpAddress.Name = "lbl_ssh_IpAddress";
            this.lbl_ssh_IpAddress.Size = new System.Drawing.Size(110, 16);
            this.lbl_ssh_IpAddress.TabIndex = 50;
            this.lbl_ssh_IpAddress.Text = "SSH Controller IP";
            // 
            // ssh_IpAddress
            // 
            this.ssh_IpAddress.Location = new System.Drawing.Point(7, 114);
            this.ssh_IpAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ssh_IpAddress.Name = "ssh_IpAddress";
            this.ssh_IpAddress.Size = new System.Drawing.Size(168, 22);
            this.ssh_IpAddress.TabIndex = 49;
            this.ssh_IpAddress.Text = "192.168.5.126";
            // 
            // lbl_WSLStatus
            // 
            this.lbl_WSLStatus.AutoSize = true;
            this.lbl_WSLStatus.Location = new System.Drawing.Point(8, 228);
            this.lbl_WSLStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_WSLStatus.Name = "lbl_WSLStatus";
            this.lbl_WSLStatus.Size = new System.Drawing.Size(117, 16);
            this.lbl_WSLStatus.TabIndex = 48;
            this.lbl_WSLStatus.Text = "Subsystem Status:";
            this.lbl_WSLStatus.Click += new System.EventHandler(this.lbl_WSLStatus_Click);
            // 
            // SubRunning
            // 
            this.SubRunning.Location = new System.Drawing.Point(11, 246);
            this.SubRunning.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SubRunning.Name = "SubRunning";
            this.SubRunning.Size = new System.Drawing.Size(167, 26);
            this.SubRunning.TabIndex = 47;
            this.SubRunning.Text = "Not Running";
            this.SubRunning.UseVisualStyleBackColor = true;
            this.SubRunning.Click += new System.EventHandler(this.SubRunning_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 16);
            this.label3.TabIndex = 46;
            this.label3.Text = "Camera Hardware ID:";
            // 
            // hardwareID
            // 
            this.hardwareID.Location = new System.Drawing.Point(8, 49);
            this.hardwareID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.hardwareID.Name = "hardwareID";
            this.hardwareID.Size = new System.Drawing.Size(167, 22);
            this.hardwareID.TabIndex = 45;
            this.hardwareID.Text = "04b0:0440";
            // 
            // btn_WSLStart
            // 
            this.btn_WSLStart.Location = new System.Drawing.Point(7, 143);
            this.btn_WSLStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_WSLStart.Name = "btn_WSLStart";
            this.btn_WSLStart.Size = new System.Drawing.Size(168, 30);
            this.btn_WSLStart.TabIndex = 44;
            this.btn_WSLStart.Text = "Start Sub System";
            this.btn_WSLStart.UseVisualStyleBackColor = true;
            this.btn_WSLStart.Click += new System.EventHandler(this.btn_WSLStart_Click);
            // 
            // btn_WSLStop
            // 
            this.btn_WSLStop.Location = new System.Drawing.Point(7, 180);
            this.btn_WSLStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_WSLStop.Name = "btn_WSLStop";
            this.btn_WSLStop.Size = new System.Drawing.Size(168, 28);
            this.btn_WSLStop.TabIndex = 43;
            this.btn_WSLStop.Text = "Stop Sub System";
            this.btn_WSLStop.UseVisualStyleBackColor = true;
            this.btn_WSLStop.Click += new System.EventHandler(this.btn_WSLStop_Click);
            // 
            // btn_ConnectCTRL
            // 
            this.btn_ConnectCTRL.Location = new System.Drawing.Point(336, 103);
            this.btn_ConnectCTRL.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ConnectCTRL.Name = "btn_ConnectCTRL";
            this.btn_ConnectCTRL.Size = new System.Drawing.Size(75, 23);
            this.btn_ConnectCTRL.TabIndex = 35;
            this.btn_ConnectCTRL.Text = "Connect";
            this.btn_ConnectCTRL.UseVisualStyleBackColor = true;
            this.btn_ConnectCTRL.Click += new System.EventHandler(this.btn_ConnectCTRL_Click);
            // 
            // listView_Controllers
            // 
            this.listView_Controllers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IPAddress,
            this.ControllerName});
            this.listView_Controllers.HideSelection = false;
            this.listView_Controllers.Location = new System.Drawing.Point(11, 33);
            this.listView_Controllers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView_Controllers.Name = "listView_Controllers";
            this.listView_Controllers.Size = new System.Drawing.Size(319, 98);
            this.listView_Controllers.TabIndex = 34;
            this.listView_Controllers.UseCompatibleStateImageBehavior = false;
            this.listView_Controllers.View = System.Windows.Forms.View.Details;
            // 
            // IPAddress
            // 
            this.IPAddress.Text = "IP Address";
            this.IPAddress.Width = 107;
            // 
            // ControllerName
            // 
            this.ControllerName.Text = "Controller Name";
            this.ControllerName.Width = 205;
            // 
            // btn_ScanCTRLS
            // 
            this.btn_ScanCTRLS.Location = new System.Drawing.Point(336, 33);
            this.btn_ScanCTRLS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ScanCTRLS.Name = "btn_ScanCTRLS";
            this.btn_ScanCTRLS.Size = new System.Drawing.Size(139, 23);
            this.btn_ScanCTRLS.TabIndex = 33;
            this.btn_ScanCTRLS.Text = "Scan Controllers";
            this.btn_ScanCTRLS.UseVisualStyleBackColor = true;
            this.btn_ScanCTRLS.Click += new System.EventHandler(this.btn_ScanCTRLS_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 427);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 21;
            this.label5.Text = "Event Log:";
            // 
            // btn_SaveLog
            // 
            this.btn_SaveLog.Location = new System.Drawing.Point(880, 514);
            this.btn_SaveLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_SaveLog.Name = "btn_SaveLog";
            this.btn_SaveLog.Size = new System.Drawing.Size(105, 30);
            this.btn_SaveLog.TabIndex = 20;
            this.btn_SaveLog.Text = "Save Log";
            this.btn_SaveLog.UseVisualStyleBackColor = true;
            this.btn_SaveLog.Click += new System.EventHandler(this.btn_SaveLog_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Location = new System.Drawing.Point(11, 446);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(863, 99);
            this.richTextBox1.TabIndex = 12;
            this.richTextBox1.Text = "";
            // 
            // cycleDataGridViewTextBoxColumn
            // 
            this.cycleDataGridViewTextBoxColumn.DataPropertyName = "Cycle";
            this.cycleDataGridViewTextBoxColumn.HeaderText = "Cycle";
            this.cycleDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.cycleDataGridViewTextBoxColumn.Name = "cycleDataGridViewTextBoxColumn";
            this.cycleDataGridViewTextBoxColumn.Width = 125;
            // 
            // enabledDataGridViewCheckBoxColumn
            // 
            this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
            this.enabledDataGridViewCheckBoxColumn.HeaderText = "Enabled";
            this.enabledDataGridViewCheckBoxColumn.MinimumWidth = 6;
            this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
            this.enabledDataGridViewCheckBoxColumn.Width = 125;
            // 
            // executionTypeDataGridViewTextBoxColumn
            // 
            this.executionTypeDataGridViewTextBoxColumn.DataPropertyName = "ExecutionType";
            this.executionTypeDataGridViewTextBoxColumn.HeaderText = "ExecutionType";
            this.executionTypeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.executionTypeDataGridViewTextBoxColumn.Name = "executionTypeDataGridViewTextBoxColumn";
            this.executionTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.executionTypeDataGridViewTextBoxColumn.Width = 125;
            // 
            // motionDataGridViewCheckBoxColumn
            // 
            this.motionDataGridViewCheckBoxColumn.DataPropertyName = "Motion";
            this.motionDataGridViewCheckBoxColumn.HeaderText = "Motion";
            this.motionDataGridViewCheckBoxColumn.MinimumWidth = 6;
            this.motionDataGridViewCheckBoxColumn.Name = "motionDataGridViewCheckBoxColumn";
            this.motionDataGridViewCheckBoxColumn.ReadOnly = true;
            this.motionDataGridViewCheckBoxColumn.Width = 125;
            // 
            // motionPointerDataGridViewTextBoxColumn
            // 
            this.motionPointerDataGridViewTextBoxColumn.DataPropertyName = "MotionPointer";
            this.motionPointerDataGridViewTextBoxColumn.HeaderText = "MotionPointer";
            this.motionPointerDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.motionPointerDataGridViewTextBoxColumn.Name = "motionPointerDataGridViewTextBoxColumn";
            this.motionPointerDataGridViewTextBoxColumn.ReadOnly = true;
            this.motionPointerDataGridViewTextBoxColumn.Width = 125;
            // 
            // programPointerDataGridViewTextBoxColumn
            // 
            this.programPointerDataGridViewTextBoxColumn.DataPropertyName = "ProgramPointer";
            this.programPointerDataGridViewTextBoxColumn.HeaderText = "ProgramPointer";
            this.programPointerDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.programPointerDataGridViewTextBoxColumn.Name = "programPointerDataGridViewTextBoxColumn";
            this.programPointerDataGridViewTextBoxColumn.ReadOnly = true;
            this.programPointerDataGridViewTextBoxColumn.Width = 125;
            // 
            // remainingCyclesDataGridViewTextBoxColumn
            // 
            this.remainingCyclesDataGridViewTextBoxColumn.DataPropertyName = "RemainingCycles";
            this.remainingCyclesDataGridViewTextBoxColumn.HeaderText = "RemainingCycles";
            this.remainingCyclesDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.remainingCyclesDataGridViewTextBoxColumn.Name = "remainingCyclesDataGridViewTextBoxColumn";
            this.remainingCyclesDataGridViewTextBoxColumn.Width = 125;
            // 
            // taskTypeDataGridViewTextBoxColumn
            // 
            this.taskTypeDataGridViewTextBoxColumn.DataPropertyName = "TaskType";
            this.taskTypeDataGridViewTextBoxColumn.HeaderText = "TaskType";
            this.taskTypeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.taskTypeDataGridViewTextBoxColumn.Name = "taskTypeDataGridViewTextBoxColumn";
            this.taskTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.taskTypeDataGridViewTextBoxColumn.Width = 125;
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            this.typeDataGridViewTextBoxColumn.HeaderText = "Type";
            this.typeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            this.typeDataGridViewTextBoxColumn.ReadOnly = true;
            this.typeDataGridViewTextBoxColumn.Width = 125;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 125;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1363, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cameraHardwareIDToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // cameraHardwareIDToolStripMenuItem
            // 
            this.cameraHardwareIDToolStripMenuItem.Name = "cameraHardwareIDToolStripMenuItem";
            this.cameraHardwareIDToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.cameraHardwareIDToolStripMenuItem.Text = "Camera Hardware ID";
            this.cameraHardwareIDToolStripMenuItem.Click += new System.EventHandler(this.cameraHardwareIDToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1363, 735);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Robot Photogrametry";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Frames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Interval)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cycleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn executionTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn motionDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn motionPointerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn programPointerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn remainingCyclesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btn_SaveLog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button btn_ScanCTRLS;
        private System.Windows.Forms.ListView listView_Controllers;
        private System.Windows.Forms.ColumnHeader IPAddress;
        private System.Windows.Forms.ColumnHeader ControllerName;
        private System.Windows.Forms.Button btn_ConnectCTRL;
        private System.Windows.Forms.NumericUpDown Frames;
        private System.Windows.Forms.NumericUpDown Interval;
        private System.Windows.Forms.Button btn_Capture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_Interval;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cameraHardwareIDToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox hardwareID;
        private System.Windows.Forms.Button btn_WSLStart;
        private System.Windows.Forms.Button btn_WSLStop;
        public System.Windows.Forms.TextBox CameraFolder;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Button SubRunning;
        private System.Windows.Forms.Label lbl_WSLStatus;
        private System.Windows.Forms.Button btn_StopRap;
        private System.Windows.Forms.Button btn_StartRAP;
        private System.Windows.Forms.Button btn_RapContinue;
        private System.Windows.Forms.Label lbl_ssh_IpAddress;
        public System.Windows.Forms.TextBox ssh_IpAddress;
        private System.Windows.Forms.Label lbl_SelectFolder;
        private System.Windows.Forms.Button btn_SelectFolder;
    }
}

