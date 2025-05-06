namespace FocalSpec.GuiExample.View
{
    partial class SetupView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonXCalibrationFile = new System.Windows.Forms.Button();
            this.TextBoxXCalibrationFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonZCalibrationFile = new System.Windows.Forms.Button();
            this.TextBoxZCalibrationFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ButtonCancel);
            this.panel1.Controls.Add(this.ButtonOk);
            this.panel1.Controls.Add(this.ButtonXCalibrationFile);
            this.panel1.Controls.Add(this.TextBoxXCalibrationFile);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ButtonZCalibrationFile);
            this.panel1.Controls.Add(this.TextBoxZCalibrationFile);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(583, 120);
            this.panel1.TabIndex = 0;
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(505, 94);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 7;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonOk
            // 
            this.ButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonOk.Enabled = false;
            this.ButtonOk.Location = new System.Drawing.Point(424, 94);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 6;
            this.ButtonOk.Text = "OK";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // ButtonXCalibrationFile
            // 
            this.ButtonXCalibrationFile.Location = new System.Drawing.Point(505, 53);
            this.ButtonXCalibrationFile.Name = "ButtonXCalibrationFile";
            this.ButtonXCalibrationFile.Size = new System.Drawing.Size(75, 23);
            this.ButtonXCalibrationFile.TabIndex = 5;
            this.ButtonXCalibrationFile.Text = "Select";
            this.ButtonXCalibrationFile.UseVisualStyleBackColor = true;
            this.ButtonXCalibrationFile.Click += new System.EventHandler(this.ButtonXCalibrationFile_Click);
            // 
            // TextBoxXCalibrationFile
            // 
            this.TextBoxXCalibrationFile.Location = new System.Drawing.Point(6, 55);
            this.TextBoxXCalibrationFile.Name = "TextBoxXCalibrationFile";
            this.TextBoxXCalibrationFile.Size = new System.Drawing.Size(493, 20);
            this.TextBoxXCalibrationFile.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "X Calibration File";
            // 
            // ButtonZCalibrationFile
            // 
            this.ButtonZCalibrationFile.Location = new System.Drawing.Point(505, 14);
            this.ButtonZCalibrationFile.Name = "ButtonZCalibrationFile";
            this.ButtonZCalibrationFile.Size = new System.Drawing.Size(75, 23);
            this.ButtonZCalibrationFile.TabIndex = 2;
            this.ButtonZCalibrationFile.Text = "Select";
            this.ButtonZCalibrationFile.UseVisualStyleBackColor = true;
            this.ButtonZCalibrationFile.Click += new System.EventHandler(this.ButtonZCalibrationFile_Click);
            // 
            // TextBoxZCalibrationFile
            // 
            this.TextBoxZCalibrationFile.Location = new System.Drawing.Point(6, 16);
            this.TextBoxZCalibrationFile.Name = "TextBoxZCalibrationFile";
            this.TextBoxZCalibrationFile.Size = new System.Drawing.Size(493, 20);
            this.TextBoxZCalibrationFile.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Z Calibration File:";
            // 
            // SetupView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 146);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupView";
            this.Text = "Setup";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ButtonXCalibrationFile;
        private System.Windows.Forms.TextBox TextBoxXCalibrationFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonZCalibrationFile;
        private System.Windows.Forms.TextBox TextBoxZCalibrationFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
    }
}