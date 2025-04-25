namespace FocalSpec.GuiExample.View
{
    partial class FilterView
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
            this.checkBoxNoiseRemoval = new System.Windows.Forms.CheckBox();
            this.checkBoxAverageZ = new System.Windows.Forms.CheckBox();
            this.checkBoxAverageIntensity = new System.Windows.Forms.CheckBox();
            this.checkBoxMedianZ = new System.Windows.Forms.CheckBox();
            this.checkBoxMedianIntensity = new System.Windows.Forms.CheckBox();
            this.checkBoxResample = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxAverageZ = new System.Windows.Forms.TextBox();
            this.textBoxAverageIntensity = new System.Windows.Forms.TextBox();
            this.textBoxMedianZ = new System.Windows.Forms.TextBox();
            this.textBoxMedianIntensity = new System.Windows.Forms.TextBox();
            this.textBoxResample = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.checkBoxPeakXfilter = new System.Windows.Forms.CheckBox();
            this.comboBoxPeakXFilter = new System.Windows.Forms.ComboBox();
            this.textBoxFillGapMax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxFillGapMax = new System.Windows.Forms.CheckBox();
            this.checkBoxTrimEdges = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxNoiseRemoval
            // 
            this.checkBoxNoiseRemoval.AutoSize = true;
            this.checkBoxNoiseRemoval.Location = new System.Drawing.Point(8, 8);
            this.checkBoxNoiseRemoval.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxNoiseRemoval.Name = "checkBoxNoiseRemoval";
            this.checkBoxNoiseRemoval.Size = new System.Drawing.Size(98, 17);
            this.checkBoxNoiseRemoval.TabIndex = 0;
            this.checkBoxNoiseRemoval.Text = "Noise Removal";
            this.checkBoxNoiseRemoval.UseVisualStyleBackColor = true;
            // 
            // checkBoxAverageZ
            // 
            this.checkBoxAverageZ.AutoSize = true;
            this.checkBoxAverageZ.Location = new System.Drawing.Point(8, 31);
            this.checkBoxAverageZ.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxAverageZ.Name = "checkBoxAverageZ";
            this.checkBoxAverageZ.Size = new System.Drawing.Size(76, 17);
            this.checkBoxAverageZ.TabIndex = 1;
            this.checkBoxAverageZ.Text = "Average Z";
            this.checkBoxAverageZ.UseVisualStyleBackColor = true;
            this.checkBoxAverageZ.CheckedChanged += new System.EventHandler(this.checkBoxAverageZ_CheckedChanged);
            // 
            // checkBoxAverageIntensity
            // 
            this.checkBoxAverageIntensity.AutoSize = true;
            this.checkBoxAverageIntensity.Location = new System.Drawing.Point(8, 54);
            this.checkBoxAverageIntensity.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxAverageIntensity.Name = "checkBoxAverageIntensity";
            this.checkBoxAverageIntensity.Size = new System.Drawing.Size(108, 17);
            this.checkBoxAverageIntensity.TabIndex = 2;
            this.checkBoxAverageIntensity.Text = "Average Intensity";
            this.checkBoxAverageIntensity.UseVisualStyleBackColor = true;
            this.checkBoxAverageIntensity.CheckedChanged += new System.EventHandler(this.checkBoxAverageIntensity_CheckedChanged);
            // 
            // checkBoxMedianZ
            // 
            this.checkBoxMedianZ.AutoSize = true;
            this.checkBoxMedianZ.Location = new System.Drawing.Point(8, 77);
            this.checkBoxMedianZ.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxMedianZ.Name = "checkBoxMedianZ";
            this.checkBoxMedianZ.Size = new System.Drawing.Size(71, 17);
            this.checkBoxMedianZ.TabIndex = 3;
            this.checkBoxMedianZ.Text = "Median Z";
            this.checkBoxMedianZ.UseVisualStyleBackColor = true;
            this.checkBoxMedianZ.CheckedChanged += new System.EventHandler(this.checkBoxMedianZ_CheckedChanged);
            // 
            // checkBoxMedianIntensity
            // 
            this.checkBoxMedianIntensity.AutoSize = true;
            this.checkBoxMedianIntensity.Location = new System.Drawing.Point(8, 100);
            this.checkBoxMedianIntensity.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxMedianIntensity.Name = "checkBoxMedianIntensity";
            this.checkBoxMedianIntensity.Size = new System.Drawing.Size(103, 17);
            this.checkBoxMedianIntensity.TabIndex = 4;
            this.checkBoxMedianIntensity.Text = "Median Intensity";
            this.checkBoxMedianIntensity.UseVisualStyleBackColor = true;
            this.checkBoxMedianIntensity.CheckedChanged += new System.EventHandler(this.checkBoxMedianIntensity_CheckedChanged);
            // 
            // checkBoxResample
            // 
            this.checkBoxResample.AutoSize = true;
            this.checkBoxResample.Location = new System.Drawing.Point(8, 123);
            this.checkBoxResample.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxResample.Name = "checkBoxResample";
            this.checkBoxResample.Size = new System.Drawing.Size(136, 17);
            this.checkBoxResample.TabIndex = 5;
            this.checkBoxResample.Text = "Resample X-Resolution";
            this.checkBoxResample.UseVisualStyleBackColor = true;
            this.checkBoxResample.CheckedChanged += new System.EventHandler(this.checkBoxResample_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "µm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "µm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(197, 125);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "µm";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(198, 79);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "pixels";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(198, 102);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "pixels";
            // 
            // textBoxAverageZ
            // 
            this.textBoxAverageZ.Location = new System.Drawing.Point(146, 29);
            this.textBoxAverageZ.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAverageZ.Name = "textBoxAverageZ";
            this.textBoxAverageZ.Size = new System.Drawing.Size(48, 20);
            this.textBoxAverageZ.TabIndex = 16;
            // 
            // textBoxAverageIntensity
            // 
            this.textBoxAverageIntensity.Location = new System.Drawing.Point(146, 52);
            this.textBoxAverageIntensity.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAverageIntensity.Name = "textBoxAverageIntensity";
            this.textBoxAverageIntensity.Size = new System.Drawing.Size(48, 20);
            this.textBoxAverageIntensity.TabIndex = 17;
            // 
            // textBoxMedianZ
            // 
            this.textBoxMedianZ.Location = new System.Drawing.Point(146, 75);
            this.textBoxMedianZ.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxMedianZ.Name = "textBoxMedianZ";
            this.textBoxMedianZ.Size = new System.Drawing.Size(48, 20);
            this.textBoxMedianZ.TabIndex = 18;
            // 
            // textBoxMedianIntensity
            // 
            this.textBoxMedianIntensity.Location = new System.Drawing.Point(146, 98);
            this.textBoxMedianIntensity.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxMedianIntensity.Name = "textBoxMedianIntensity";
            this.textBoxMedianIntensity.Size = new System.Drawing.Size(48, 20);
            this.textBoxMedianIntensity.TabIndex = 19;
            // 
            // textBoxResample
            // 
            this.textBoxResample.Location = new System.Drawing.Point(146, 121);
            this.textBoxResample.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxResample.Name = "textBoxResample";
            this.textBoxResample.Size = new System.Drawing.Size(48, 20);
            this.textBoxResample.TabIndex = 20;
            this.textBoxResample.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxResample_Validating);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(181, 220);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(50, 24);
            this.buttonCancel.TabIndex = 21;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(127, 220);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(2);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(50, 24);
            this.buttonOk.TabIndex = 22;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // checkBoxPeakXfilter
            // 
            this.checkBoxPeakXfilter.AutoSize = true;
            this.checkBoxPeakXfilter.Location = new System.Drawing.Point(8, 146);
            this.checkBoxPeakXfilter.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxPeakXfilter.Name = "checkBoxPeakXfilter";
            this.checkBoxPeakXfilter.Size = new System.Drawing.Size(122, 17);
            this.checkBoxPeakXfilter.TabIndex = 23;
            this.checkBoxPeakXfilter.Text = "Peak X-Filter Length";
            this.checkBoxPeakXfilter.UseVisualStyleBackColor = true;
            this.checkBoxPeakXfilter.CheckedChanged += new System.EventHandler(this.checkBoxPeakXFilter_CheckedChanged);
            // 
            // comboBoxPeakXFilter
            // 
            this.comboBoxPeakXFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPeakXFilter.FormattingEnabled = true;
            this.comboBoxPeakXFilter.Items.AddRange(new object[] {
            "3",
            "5",
            "7"});
            this.comboBoxPeakXFilter.Location = new System.Drawing.Point(146, 144);
            this.comboBoxPeakXFilter.Name = "comboBoxPeakXFilter";
            this.comboBoxPeakXFilter.Size = new System.Drawing.Size(48, 21);
            this.comboBoxPeakXFilter.TabIndex = 26;
            // 
            // textBoxFillGapMax
            // 
            this.textBoxFillGapMax.Location = new System.Drawing.Point(146, 169);
            this.textBoxFillGapMax.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxFillGapMax.Name = "textBoxFillGapMax";
            this.textBoxFillGapMax.Size = new System.Drawing.Size(48, 20);
            this.textBoxFillGapMax.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(198, 173);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "µm";
            // 
            // checkBoxFillGapMax
            // 
            this.checkBoxFillGapMax.AutoSize = true;
            this.checkBoxFillGapMax.Location = new System.Drawing.Point(8, 169);
            this.checkBoxFillGapMax.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxFillGapMax.Name = "checkBoxFillGapMax";
            this.checkBoxFillGapMax.Size = new System.Drawing.Size(84, 17);
            this.checkBoxFillGapMax.TabIndex = 27;
            this.checkBoxFillGapMax.Text = "Fill Gap Max";
            this.checkBoxFillGapMax.UseVisualStyleBackColor = true;
            this.checkBoxFillGapMax.CheckedChanged += new System.EventHandler(this.checkBoxFillGapMax_CheckedChanged);
            // 
            // checkBoxTrimEdges
            // 
            this.checkBoxTrimEdges.AutoSize = true;
            this.checkBoxTrimEdges.Location = new System.Drawing.Point(8, 192);
            this.checkBoxTrimEdges.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxTrimEdges.Name = "checkBoxTrimEdges";
            this.checkBoxTrimEdges.Size = new System.Drawing.Size(79, 17);
            this.checkBoxTrimEdges.TabIndex = 30;
            this.checkBoxTrimEdges.Text = "Trim Edges";
            this.checkBoxTrimEdges.UseVisualStyleBackColor = true;
            // 
            // FilterView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 253);
            this.Controls.Add(this.checkBoxTrimEdges);
            this.Controls.Add(this.textBoxFillGapMax);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.checkBoxFillGapMax);
            this.Controls.Add(this.comboBoxPeakXFilter);
            this.Controls.Add(this.checkBoxPeakXfilter);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxResample);
            this.Controls.Add(this.textBoxMedianIntensity);
            this.Controls.Add(this.textBoxMedianZ);
            this.Controls.Add(this.textBoxAverageIntensity);
            this.Controls.Add(this.textBoxAverageZ);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxResample);
            this.Controls.Add(this.checkBoxMedianIntensity);
            this.Controls.Add(this.checkBoxMedianZ);
            this.Controls.Add(this.checkBoxAverageIntensity);
            this.Controls.Add(this.checkBoxAverageZ);
            this.Controls.Add(this.checkBoxNoiseRemoval);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Filter Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FilterView_FormClosing);
            this.Shown += new System.EventHandler(this.FilterView_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxNoiseRemoval;
        private System.Windows.Forms.CheckBox checkBoxAverageZ;
        private System.Windows.Forms.CheckBox checkBoxAverageIntensity;
        private System.Windows.Forms.CheckBox checkBoxMedianZ;
        private System.Windows.Forms.CheckBox checkBoxMedianIntensity;
        private System.Windows.Forms.CheckBox checkBoxResample;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxAverageZ;
        private System.Windows.Forms.TextBox textBoxAverageIntensity;
        private System.Windows.Forms.TextBox textBoxMedianZ;
        private System.Windows.Forms.TextBox textBoxMedianIntensity;
        private System.Windows.Forms.TextBox textBoxResample;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.CheckBox checkBoxPeakXfilter;
        private System.Windows.Forms.ComboBox comboBoxPeakXFilter;
        private System.Windows.Forms.TextBox textBoxFillGapMax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxFillGapMax;
        private System.Windows.Forms.CheckBox checkBoxTrimEdges;
    }
}