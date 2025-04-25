namespace FocalSpec.GuiExample.View
{
    partial class AdvancedView
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
            this.checkBoxHdrEnabled = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownVLow2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownVLow3 = new System.Windows.Forms.NumericUpDown();
            this.TextVLow3 = new System.Windows.Forms.Label();
            this.numericUpDownKp1Pos = new System.Windows.Forms.NumericUpDown();
            this.TextKp1Pos = new System.Windows.Forms.Label();
            this.numericUpDownKp2Pos = new System.Windows.Forms.NumericUpDown();
            this.TextKp2Pos = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.comboBoxLayer = new System.Windows.Forms.ComboBox();
            this.CheckBoxLayerParameters = new System.Windows.Forms.CheckBox();
            this.comboBoxIntensityType = new System.Windows.Forms.ComboBox();
            this.labelIntensityType = new System.Windows.Forms.Label();
            this.textBoxThickness = new System.Windows.Forms.TextBox();
            this.labelThickness = new System.Windows.Forms.Label();
            this.labelLayer = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVLow2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVLow3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKp1Pos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKp2Pos)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxHdrEnabled
            // 
            this.checkBoxHdrEnabled.AutoSize = true;
            this.checkBoxHdrEnabled.Location = new System.Drawing.Point(13, 13);
            this.checkBoxHdrEnabled.Name = "checkBoxHdrEnabled";
            this.checkBoxHdrEnabled.Size = new System.Drawing.Size(85, 17);
            this.checkBoxHdrEnabled.TabIndex = 0;
            this.checkBoxHdrEnabled.Text = "Hdr Enabled";
            this.checkBoxHdrEnabled.UseVisualStyleBackColor = true;
            this.checkBoxHdrEnabled.Click += new System.EventHandler(this.checkBoxHdrEnabled_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "VLow2";
            // 
            // numericUpDownVLow2
            // 
            this.numericUpDownVLow2.DecimalPlaces = 2;
            this.numericUpDownVLow2.Location = new System.Drawing.Point(83, 33);
            this.numericUpDownVLow2.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.numericUpDownVLow2.Minimum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numericUpDownVLow2.Name = "numericUpDownVLow2";
            this.numericUpDownVLow2.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownVLow2.TabIndex = 2;
            this.numericUpDownVLow2.Value = new decimal(new int[] {
            114,
            0,
            0,
            0});
            // 
            // numericUpDownVLow3
            // 
            this.numericUpDownVLow3.DecimalPlaces = 2;
            this.numericUpDownVLow3.Location = new System.Drawing.Point(83, 60);
            this.numericUpDownVLow3.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.numericUpDownVLow3.Minimum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numericUpDownVLow3.Name = "numericUpDownVLow3";
            this.numericUpDownVLow3.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownVLow3.TabIndex = 4;
            this.numericUpDownVLow3.Value = new decimal(new int[] {
            116,
            0,
            0,
            0});
            // 
            // TextVLow3
            // 
            this.TextVLow3.AutoSize = true;
            this.TextVLow3.Location = new System.Drawing.Point(29, 64);
            this.TextVLow3.Name = "TextVLow3";
            this.TextVLow3.Size = new System.Drawing.Size(40, 13);
            this.TextVLow3.TabIndex = 3;
            this.TextVLow3.Text = "VLow3";
            // 
            // numericUpDownKp1Pos
            // 
            this.numericUpDownKp1Pos.DecimalPlaces = 2;
            this.numericUpDownKp1Pos.Location = new System.Drawing.Point(83, 87);
            this.numericUpDownKp1Pos.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownKp1Pos.Name = "numericUpDownKp1Pos";
            this.numericUpDownKp1Pos.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownKp1Pos.TabIndex = 6;
            this.numericUpDownKp1Pos.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // TextKp1Pos
            // 
            this.TextKp1Pos.AutoSize = true;
            this.TextKp1Pos.Location = new System.Drawing.Point(29, 91);
            this.TextKp1Pos.Name = "TextKp1Pos";
            this.TextKp1Pos.Size = new System.Drawing.Size(44, 13);
            this.TextKp1Pos.TabIndex = 5;
            this.TextKp1Pos.Text = "Kp1Pos";
            // 
            // numericUpDownKp2Pos
            // 
            this.numericUpDownKp2Pos.DecimalPlaces = 2;
            this.numericUpDownKp2Pos.Location = new System.Drawing.Point(83, 113);
            this.numericUpDownKp2Pos.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownKp2Pos.Name = "numericUpDownKp2Pos";
            this.numericUpDownKp2Pos.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownKp2Pos.TabIndex = 8;
            this.numericUpDownKp2Pos.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // TextKp2Pos
            // 
            this.TextKp2Pos.AutoSize = true;
            this.TextKp2Pos.Location = new System.Drawing.Point(29, 115);
            this.TextKp2Pos.Name = "TextKp2Pos";
            this.TextKp2Pos.Size = new System.Drawing.Size(44, 13);
            this.TextKp2Pos.TabIndex = 7;
            this.TextKp2Pos.Text = "Kp2Pos";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(296, 136);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 9;
            this.buttonOk.Text = "Close";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // comboBoxLayer
            // 
            this.comboBoxLayer.FormattingEnabled = true;
            this.comboBoxLayer.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comboBoxLayer.Location = new System.Drawing.Point(250, 33);
            this.comboBoxLayer.Name = "comboBoxLayer";
            this.comboBoxLayer.Size = new System.Drawing.Size(49, 21);
            this.comboBoxLayer.TabIndex = 20;
            this.comboBoxLayer.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLayer_SelectedIndexChanged);
            // 
            // CheckBoxLayerParameters
            // 
            this.CheckBoxLayerParameters.AutoSize = true;
            this.CheckBoxLayerParameters.Location = new System.Drawing.Point(154, 13);
            this.CheckBoxLayerParameters.Name = "CheckBoxLayerParameters";
            this.CheckBoxLayerParameters.Size = new System.Drawing.Size(146, 17);
            this.CheckBoxLayerParameters.TabIndex = 19;
            this.CheckBoxLayerParameters.Text = "Layer specific parameters";
            this.CheckBoxLayerParameters.UseVisualStyleBackColor = true;
            this.CheckBoxLayerParameters.CheckedChanged += new System.EventHandler(this.CheckBoxLayerParameters_CheckedChanged);
            // 
            // comboBoxIntensityType
            // 
            this.comboBoxIntensityType.FormattingEnabled = true;
            this.comboBoxIntensityType.Items.AddRange(new object[] {
            "Average intensity",
            "Peak detection"});
            this.comboBoxIntensityType.Location = new System.Drawing.Point(250, 87);
            this.comboBoxIntensityType.Name = "comboBoxIntensityType";
            this.comboBoxIntensityType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxIntensityType.TabIndex = 18;
            // 
            // labelIntensityType
            // 
            this.labelIntensityType.AutoSize = true;
            this.labelIntensityType.Location = new System.Drawing.Point(173, 91);
            this.labelIntensityType.Name = "labelIntensityType";
            this.labelIntensityType.Size = new System.Drawing.Size(73, 13);
            this.labelIntensityType.TabIndex = 17;
            this.labelIntensityType.Text = "Intensity Type";
            // 
            // textBoxThickness
            // 
            this.textBoxThickness.Location = new System.Drawing.Point(250, 60);
            this.textBoxThickness.Name = "textBoxThickness";
            this.textBoxThickness.Size = new System.Drawing.Size(49, 20);
            this.textBoxThickness.TabIndex = 16;
            // 
            // labelThickness
            // 
            this.labelThickness.AutoSize = true;
            this.labelThickness.Location = new System.Drawing.Point(173, 64);
            this.labelThickness.Name = "labelThickness";
            this.labelThickness.Size = new System.Drawing.Size(79, 13);
            this.labelThickness.TabIndex = 15;
            this.labelThickness.Text = "Max Thickness";
            // 
            // labelLayer
            // 
            this.labelLayer.AutoSize = true;
            this.labelLayer.Location = new System.Drawing.Point(173, 37);
            this.labelLayer.Name = "labelLayer";
            this.labelLayer.Size = new System.Drawing.Size(33, 13);
            this.labelLayer.TabIndex = 14;
            this.labelLayer.Text = "Layer";
            // 
            // AdvancedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 171);
            this.Controls.Add(this.comboBoxLayer);
            this.Controls.Add(this.CheckBoxLayerParameters);
            this.Controls.Add(this.comboBoxIntensityType);
            this.Controls.Add(this.labelIntensityType);
            this.Controls.Add(this.textBoxThickness);
            this.Controls.Add(this.labelThickness);
            this.Controls.Add(this.labelLayer);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.numericUpDownKp2Pos);
            this.Controls.Add(this.TextKp2Pos);
            this.Controls.Add(this.numericUpDownKp1Pos);
            this.Controls.Add(this.TextKp1Pos);
            this.Controls.Add(this.numericUpDownVLow3);
            this.Controls.Add(this.TextVLow3);
            this.Controls.Add(this.numericUpDownVLow2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxHdrEnabled);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Advanced Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdvancedView_FormClosing);
            this.Shown += new System.EventHandler(this.AdvancedView_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVLow2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVLow3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKp1Pos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKp2Pos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxHdrEnabled;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownVLow2;
        private System.Windows.Forms.NumericUpDown numericUpDownVLow3;
        private System.Windows.Forms.Label TextVLow3;
        private System.Windows.Forms.NumericUpDown numericUpDownKp1Pos;
        private System.Windows.Forms.Label TextKp1Pos;
        private System.Windows.Forms.NumericUpDown numericUpDownKp2Pos;
        private System.Windows.Forms.Label TextKp2Pos;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ComboBox comboBoxLayer;
        private System.Windows.Forms.CheckBox CheckBoxLayerParameters;
        private System.Windows.Forms.ComboBox comboBoxIntensityType;
        private System.Windows.Forms.Label labelIntensityType;
        private System.Windows.Forms.TextBox textBoxThickness;
        private System.Windows.Forms.Label labelThickness;
        private System.Windows.Forms.Label labelLayer;
    }
}