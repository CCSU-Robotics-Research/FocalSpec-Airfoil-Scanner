namespace FocalSpec.GuiExample.View
{
    partial class PeakDetectionView
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
            this.comboBoxAverageIntensityFilter = new System.Windows.Forms.ComboBox();
            this.labelAverageIntensityFilter = new System.Windows.Forms.Label();
            this.numericUpDownPeakCoreThreshold = new System.Windows.Forms.NumericUpDown();
            this.comboBoxAveragingFirLength = new System.Windows.Forms.ComboBox();
            this.comboBoxFirLength = new System.Windows.Forms.ComboBox();
            this.labelAverageFir = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxDetectionFilter = new System.Windows.Forms.ComboBox();
            this.labelDetectionFilter = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPeakCoreThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxAverageIntensityFilter
            // 
            this.comboBoxAverageIntensityFilter.FormattingEnabled = true;
            this.comboBoxAverageIntensityFilter.Items.AddRange(new object[] {
            "2",
            "4",
            "6",
            "8",
            "10",
            "12",
            "14",
            "16"});
            this.comboBoxAverageIntensityFilter.Location = new System.Drawing.Point(143, 87);
            this.comboBoxAverageIntensityFilter.Name = "comboBoxAverageIntensityFilter";
            this.comboBoxAverageIntensityFilter.Size = new System.Drawing.Size(62, 21);
            this.comboBoxAverageIntensityFilter.TabIndex = 26;
            // 
            // labelAverageIntensityFilter
            // 
            this.labelAverageIntensityFilter.AutoSize = true;
            this.labelAverageIntensityFilter.Location = new System.Drawing.Point(12, 91);
            this.labelAverageIntensityFilter.Name = "labelAverageIntensityFilter";
            this.labelAverageIntensityFilter.Size = new System.Drawing.Size(114, 13);
            this.labelAverageIntensityFilter.TabIndex = 25;
            this.labelAverageIntensityFilter.Text = "Average Intensity Filter";
            // 
            // numericUpDownPeakCoreThreshold
            // 
            this.numericUpDownPeakCoreThreshold.Location = new System.Drawing.Point(143, 7);
            this.numericUpDownPeakCoreThreshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownPeakCoreThreshold.Name = "numericUpDownPeakCoreThreshold";
            this.numericUpDownPeakCoreThreshold.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownPeakCoreThreshold.TabIndex = 24;
            // 
            // comboBoxAveragingFirLength
            // 
            this.comboBoxAveragingFirLength.FormattingEnabled = true;
            this.comboBoxAveragingFirLength.Items.AddRange(new object[] {
            "2",
            "4",
            "6",
            "8",
            "10",
            "12",
            "14",
            "16"});
            this.comboBoxAveragingFirLength.Location = new System.Drawing.Point(143, 60);
            this.comboBoxAveragingFirLength.Name = "comboBoxAveragingFirLength";
            this.comboBoxAveragingFirLength.Size = new System.Drawing.Size(62, 21);
            this.comboBoxAveragingFirLength.TabIndex = 23;
            // 
            // comboBoxFirLength
            // 
            this.comboBoxFirLength.FormattingEnabled = true;
            this.comboBoxFirLength.Items.AddRange(new object[] {
            "2",
            "4",
            "6",
            "8",
            "10",
            "12",
            "14",
            "16"});
            this.comboBoxFirLength.Location = new System.Drawing.Point(143, 33);
            this.comboBoxFirLength.Name = "comboBoxFirLength";
            this.comboBoxFirLength.Size = new System.Drawing.Size(62, 21);
            this.comboBoxFirLength.TabIndex = 22;
            // 
            // labelAverageFir
            // 
            this.labelAverageFir.AutoSize = true;
            this.labelAverageFir.Location = new System.Drawing.Point(12, 64);
            this.labelAverageFir.Name = "labelAverageFir";
            this.labelAverageFir.Size = new System.Drawing.Size(105, 13);
            this.labelAverageFir.TabIndex = 21;
            this.labelAverageFir.Text = "Averaging Fir Length";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Fir Length";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Peak Threshold";
            // 
            // comboBoxDetectionFilter
            // 
            this.comboBoxDetectionFilter.FormattingEnabled = true;
            this.comboBoxDetectionFilter.Items.AddRange(new object[] {
            "2",
            "4",
            "6",
            "8",
            "10",
            "12",
            "14",
            "16"});
            this.comboBoxDetectionFilter.Location = new System.Drawing.Point(143, 60);
            this.comboBoxDetectionFilter.Name = "comboBoxDetectionFilter";
            this.comboBoxDetectionFilter.Size = new System.Drawing.Size(62, 21);
            this.comboBoxDetectionFilter.TabIndex = 28;
            // 
            // labelDetectionFilter
            // 
            this.labelDetectionFilter.AutoSize = true;
            this.labelDetectionFilter.Location = new System.Drawing.Point(12, 64);
            this.labelDetectionFilter.Name = "labelDetectionFilter";
            this.labelDetectionFilter.Size = new System.Drawing.Size(78, 13);
            this.labelDetectionFilter.TabIndex = 27;
            this.labelDetectionFilter.Text = "Detection Filter";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(131, 126);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 29;
            this.buttonOk.Text = "Apply";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(19, 126);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 30;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // PeakDetectionView
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(222, 163);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.comboBoxDetectionFilter);
            this.Controls.Add(this.labelDetectionFilter);
            this.Controls.Add(this.comboBoxAverageIntensityFilter);
            this.Controls.Add(this.labelAverageIntensityFilter);
            this.Controls.Add(this.numericUpDownPeakCoreThreshold);
            this.Controls.Add(this.comboBoxAveragingFirLength);
            this.Controls.Add(this.comboBoxFirLength);
            this.Controls.Add(this.labelAverageFir);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PeakDetectionView";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Peak Detection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PeakDetectionView_FormClosing);
            this.Shown += new System.EventHandler(this.PeakDetectionView_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPeakCoreThreshold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ComboBox comboBoxAverageIntensityFilter;
        private System.Windows.Forms.Label labelAverageIntensityFilter;
        private System.Windows.Forms.NumericUpDown numericUpDownPeakCoreThreshold;
        private System.Windows.Forms.ComboBox comboBoxAveragingFirLength;
        private System.Windows.Forms.ComboBox comboBoxFirLength;
        private System.Windows.Forms.Label labelAverageFir;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxDetectionFilter;
        private System.Windows.Forms.Label labelDetectionFilter;
        private System.Windows.Forms.Button buttonCancel;
    }
}