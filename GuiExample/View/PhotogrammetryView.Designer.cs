namespace Photogrammetry
{
    partial class PhotogrammetryView
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
            this.btn_RapContinue = new System.Windows.Forms.Button();
            this.btn_StopRap = new System.Windows.Forms.Button();
            this.btn_StartRAP = new System.Windows.Forms.Button();
            this.btn_ConnectCTRL = new System.Windows.Forms.Button();
            this.listView_Controllers = new System.Windows.Forms.ListView();
            this.IPAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ControllerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_ScanCTRLS = new System.Windows.Forms.Button();
            this.btn_SaveLog = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btn_RapContinue
            // 
            this.btn_RapContinue.Location = new System.Drawing.Point(193, 94);
            this.btn_RapContinue.Margin = new System.Windows.Forms.Padding(2);
            this.btn_RapContinue.Name = "btn_RapContinue";
            this.btn_RapContinue.Size = new System.Drawing.Size(58, 19);
            this.btn_RapContinue.TabIndex = 53;
            this.btn_RapContinue.Text = "Continue";
            this.btn_RapContinue.UseVisualStyleBackColor = true;
            this.btn_RapContinue.Click += new System.EventHandler(this.btn_RapContinue_Click);
            // 
            // btn_StopRap
            // 
            this.btn_StopRap.Location = new System.Drawing.Point(108, 94);
            this.btn_StopRap.Margin = new System.Windows.Forms.Padding(2);
            this.btn_StopRap.Name = "btn_StopRap";
            this.btn_StopRap.Size = new System.Drawing.Size(80, 19);
            this.btn_StopRap.TabIndex = 51;
            this.btn_StopRap.Text = "Stop Rapid";
            this.btn_StopRap.UseVisualStyleBackColor = true;
            this.btn_StopRap.Click += new System.EventHandler(this.btn_StopRap_Click);
            // 
            // btn_StartRAP
            // 
            this.btn_StartRAP.Location = new System.Drawing.Point(13, 95);
            this.btn_StartRAP.Margin = new System.Windows.Forms.Padding(2);
            this.btn_StartRAP.Name = "btn_StartRAP";
            this.btn_StartRAP.Size = new System.Drawing.Size(90, 19);
            this.btn_StartRAP.TabIndex = 52;
            this.btn_StartRAP.Text = "Start Rapid";
            this.btn_StartRAP.UseVisualStyleBackColor = true;
            this.btn_StartRAP.Click += new System.EventHandler(this.btn_StartRAP_Click);
            // 
            // btn_ConnectCTRL
            // 
            this.btn_ConnectCTRL.Location = new System.Drawing.Point(255, 68);
            this.btn_ConnectCTRL.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ConnectCTRL.Name = "btn_ConnectCTRL";
            this.btn_ConnectCTRL.Size = new System.Drawing.Size(104, 19);
            this.btn_ConnectCTRL.TabIndex = 50;
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
            this.listView_Controllers.Location = new System.Drawing.Point(11, 11);
            this.listView_Controllers.Margin = new System.Windows.Forms.Padding(2);
            this.listView_Controllers.Name = "listView_Controllers";
            this.listView_Controllers.Size = new System.Drawing.Size(240, 80);
            this.listView_Controllers.TabIndex = 49;
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
            this.btn_ScanCTRLS.Location = new System.Drawing.Point(255, 11);
            this.btn_ScanCTRLS.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ScanCTRLS.Name = "btn_ScanCTRLS";
            this.btn_ScanCTRLS.Size = new System.Drawing.Size(104, 19);
            this.btn_ScanCTRLS.TabIndex = 48;
            this.btn_ScanCTRLS.Text = "Scan Controllers";
            this.btn_ScanCTRLS.UseVisualStyleBackColor = true;
            this.btn_ScanCTRLS.Click += new System.EventHandler(this.btn_ScanCTRLS_Click);
            // 
            // btn_SaveLog
            // 
            this.btn_SaveLog.Location = new System.Drawing.Point(663, 402);
            this.btn_SaveLog.Margin = new System.Windows.Forms.Padding(2);
            this.btn_SaveLog.Name = "btn_SaveLog";
            this.btn_SaveLog.Size = new System.Drawing.Size(79, 24);
            this.btn_SaveLog.TabIndex = 47;
            this.btn_SaveLog.Text = "Save Log";
            this.btn_SaveLog.UseVisualStyleBackColor = true;
            this.btn_SaveLog.Click += new System.EventHandler(this.btn_SaveLog_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(11, 293);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(648, 134);
            this.richTextBox1.TabIndex = 46;
            this.richTextBox1.Text = "";
            // 
            // PhotogrammetryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_RapContinue);
            this.Controls.Add(this.btn_StopRap);
            this.Controls.Add(this.btn_StartRAP);
            this.Controls.Add(this.btn_ConnectCTRL);
            this.Controls.Add(this.listView_Controllers);
            this.Controls.Add(this.btn_ScanCTRLS);
            this.Controls.Add(this.btn_SaveLog);
            this.Controls.Add(this.richTextBox1);
            this.Name = "PhotogrammetryView";
            this.Text = "Photogrammetry";
            this.Load += new System.EventHandler(this.PhotogrammetryView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_RapContinue;
        private System.Windows.Forms.Button btn_StopRap;
        private System.Windows.Forms.Button btn_StartRAP;
        private System.Windows.Forms.Button btn_ConnectCTRL;
        private System.Windows.Forms.ListView listView_Controllers;
        private System.Windows.Forms.ColumnHeader IPAddress;
        private System.Windows.Forms.ColumnHeader ControllerName;
        private System.Windows.Forms.Button btn_ScanCTRLS;
        private System.Windows.Forms.Button btn_SaveLog;
        public System.Windows.Forms.RichTextBox richTextBox1;
    }
}