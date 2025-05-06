namespace FocalSpec.GuiExample.View
{
    partial class BatchConfigurationView
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
            this.TableLayoutPanelSettings = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxLineSpeed = new System.Windows.Forms.TextBox();
            this.TextBoxTriggerFreq = new System.Windows.Forms.TextBox();
            this.ComboBoxTrigger = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.TextBoxBatchLength = new System.Windows.Forms.TextBox();
            this.TextBoxScanStepLength = new System.Windows.Forms.TextBox();
            this.TextBoxMaxScanLength = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this._ok = new System.Windows.Forms.Button();
            this._cancel = new System.Windows.Forms.Button();
            this.TableLayoutPanelSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanelSettings
            // 
            this.TableLayoutPanelSettings.ColumnCount = 2;
            this.TableLayoutPanelSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanelSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanelSettings.Controls.Add(this.label1, 0, 0);
            this.TableLayoutPanelSettings.Controls.Add(this.TextBoxLineSpeed, 1, 2);
            this.TableLayoutPanelSettings.Controls.Add(this.TextBoxTriggerFreq, 1, 3);
            this.TableLayoutPanelSettings.Controls.Add(this.ComboBoxTrigger, 1, 0);
            this.TableLayoutPanelSettings.Controls.Add(this.label4, 0, 3);
            this.TableLayoutPanelSettings.Controls.Add(this.label3, 0, 2);
            this.TableLayoutPanelSettings.Controls.Add(this.label9, 0, 1);
            this.TableLayoutPanelSettings.Controls.Add(this.TextBoxBatchLength, 1, 1);
            this.TableLayoutPanelSettings.Controls.Add(this.TextBoxScanStepLength, 1, 4);
            this.TableLayoutPanelSettings.Controls.Add(this.TextBoxMaxScanLength, 1, 5);
            this.TableLayoutPanelSettings.Controls.Add(this.label2, 0, 4);
            this.TableLayoutPanelSettings.Controls.Add(this.label8, 0, 5);
            this.TableLayoutPanelSettings.Location = new System.Drawing.Point(12, 12);
            this.TableLayoutPanelSettings.Name = "TableLayoutPanelSettings";
            this.TableLayoutPanelSettings.RowCount = 7;
            this.TableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanelSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanelSettings.Size = new System.Drawing.Size(363, 160);
            this.TableLayoutPanelSettings.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sensor Trigger";
            // 
            // TextBoxLineSpeed
            // 
            this.TextBoxLineSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBoxLineSpeed.Location = new System.Drawing.Point(184, 56);
            this.TextBoxLineSpeed.Name = "TextBoxLineSpeed";
            this.TextBoxLineSpeed.Size = new System.Drawing.Size(176, 20);
            this.TextBoxLineSpeed.TabIndex = 2;
            this.TextBoxLineSpeed.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxLineSpeed_KeyUp);
            // 
            // TextBoxTriggerFreq
            // 
            this.TextBoxTriggerFreq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBoxTriggerFreq.Location = new System.Drawing.Point(184, 82);
            this.TextBoxTriggerFreq.Name = "TextBoxTriggerFreq";
            this.TextBoxTriggerFreq.Size = new System.Drawing.Size(176, 20);
            this.TextBoxTriggerFreq.TabIndex = 3;
            this.TextBoxTriggerFreq.Enter += new System.EventHandler(this.TextBoxTriggerFreq_Enter);
            this.TextBoxTriggerFreq.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxTriggerFreq_KeyUp);
            // 
            // ComboBoxTrigger
            // 
            this.ComboBoxTrigger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComboBoxTrigger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxTrigger.FormattingEnabled = true;
            this.ComboBoxTrigger.Items.AddRange(new object[] {
            "External",
            "Internal"});
            this.ComboBoxTrigger.Location = new System.Drawing.Point(184, 3);
            this.ComboBoxTrigger.Name = "ComboBoxTrigger";
            this.ComboBoxTrigger.Size = new System.Drawing.Size(176, 21);
            this.ComboBoxTrigger.TabIndex = 1;
            this.ComboBoxTrigger.SelectedIndexChanged += new System.EventHandler(this._sensorTrigger_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Trigger Freq [Hz]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Line Speed [m/min]";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Batch Length [N]";
            // 
            // TextBoxBatchLength
            // 
            this.TextBoxBatchLength.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBoxBatchLength.Location = new System.Drawing.Point(184, 30);
            this.TextBoxBatchLength.Name = "TextBoxBatchLength";
            this.TextBoxBatchLength.Size = new System.Drawing.Size(176, 20);
            this.TextBoxBatchLength.TabIndex = 14;
            this.TextBoxBatchLength.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxBatchLength_KeyUp);
            // 
            // TextBoxScanStepLength
            // 
            this.TextBoxScanStepLength.Location = new System.Drawing.Point(184, 108);
            this.TextBoxScanStepLength.Name = "TextBoxScanStepLength";
            this.TextBoxScanStepLength.Size = new System.Drawing.Size(176, 20);
            this.TextBoxScanStepLength.TabIndex = 5;
            this.TextBoxScanStepLength.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxScanStepLength_KeyUp);
            // 
            // TextBoxMaxScanLength
            // 
            this.TextBoxMaxScanLength.Enabled = false;
            this.TextBoxMaxScanLength.Location = new System.Drawing.Point(184, 134);
            this.TextBoxMaxScanLength.Name = "TextBoxMaxScanLength";
            this.TextBoxMaxScanLength.Size = new System.Drawing.Size(176, 20);
            this.TextBoxMaxScanLength.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Scan Step Length [mm]";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Max. Scan Length [mm]";
            // 
            // _ok
            // 
            this._ok.Location = new System.Drawing.Point(219, 181);
            this._ok.Name = "_ok";
            this._ok.Size = new System.Drawing.Size(75, 23);
            this._ok.TabIndex = 6;
            this._ok.Text = "OK";
            this._ok.UseVisualStyleBackColor = true;
            this._ok.Click += new System.EventHandler(this._ok_Click);
            // 
            // _cancel
            // 
            this._cancel.Location = new System.Drawing.Point(300, 181);
            this._cancel.Name = "_cancel";
            this._cancel.Size = new System.Drawing.Size(75, 23);
            this._cancel.TabIndex = 7;
            this._cancel.Text = "Cancel";
            this._cancel.UseVisualStyleBackColor = true;
            this._cancel.Click += new System.EventHandler(this._cancel_Click);
            // 
            // BatchConfigurationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 210);
            this.Controls.Add(this._cancel);
            this.Controls.Add(this._ok);
            this.Controls.Add(this.TableLayoutPanelSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BatchConfigurationView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Batch Configuration";
            this.TableLayoutPanelSettings.ResumeLayout(false);
            this.TableLayoutPanelSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TableLayoutPanelSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextBoxLineSpeed;
        private System.Windows.Forms.TextBox TextBoxTriggerFreq;
        private System.Windows.Forms.ComboBox ComboBoxTrigger;
        private System.Windows.Forms.TextBox TextBoxScanStepLength;
        private System.Windows.Forms.Button _ok;
        private System.Windows.Forms.Button _cancel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TextBoxMaxScanLength;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TextBoxBatchLength;
    }
}