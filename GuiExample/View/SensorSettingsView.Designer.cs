namespace FocalSpec.GuiExample.View
{
    partial class SensorSettingsView
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
            this.groupboxViewSettings = new System.Windows.Forms.GroupBox();
            this.buttonRefraction = new System.Windows.Forms.Button();
            this.checkBoxThickness = new System.Windows.Forms.CheckBox();
            this.numericUpDownWindowSize = new System.Windows.Forms.NumericUpDown();
            this.checkBoxIntensity = new System.Windows.Forms.CheckBox();
            this.checkBoxRawImage = new System.Windows.Forms.CheckBox();
            this.radioButtonGraphUnitUm = new System.Windows.Forms.RadioButton();
            this.radioButtonGraphUnitMm = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupboxSurface = new System.Windows.Forms.GroupBox();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.comboBoxTop = new System.Windows.Forms.ComboBox();
            this.comboBoxBottom = new System.Windows.Forms.ComboBox();
            this.comboBoxBrightest = new System.Windows.Forms.ComboBox();
            this.radioButtonExportAll = new System.Windows.Forms.RadioButton();
            this.buttonExportPeakData = new System.Windows.Forms.Button();
            this.radioButtonExportBrightest = new System.Windows.Forms.RadioButton();
            this.radioButtonExportBottom = new System.Windows.Forms.RadioButton();
            this.radioButtonExportTop = new System.Windows.Forms.RadioButton();
            this.groupboxSensorSettings = new System.Windows.Forms.GroupBox();
            this.textBoxMinThickness = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxSensitivity = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxMaterialType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonPeakDetection = new System.Windows.Forms.Button();
            this.buttonAdvanced = new System.Windows.Forms.Button();
            this.checkBoxHeightZeroAdjust = new System.Windows.Forms.CheckBox();
            this.textBoxAgcTargetIntensity = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxExternalPulsing = new System.Windows.Forms.CheckBox();
            this.checkBoxAgcEnabled = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAverageIntensity = new System.Windows.Forms.TextBox();
            this.comboboxFrequency = new System.Windows.Forms.ComboBox();
            this.textBoxMaxPulseWidth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            this.comboboxLedPulseWidth = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._batchMode = new FocalSpec.GuiExample.View.BatchModePresenter();
            this.groupboxViewSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindowSize)).BeginInit();
            this.groupboxSurface.SuspendLayout();
            this.groupboxSensorSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupboxViewSettings
            // 
            this.groupboxViewSettings.Controls.Add(this.buttonRefraction);
            this.groupboxViewSettings.Controls.Add(this.checkBoxThickness);
            this.groupboxViewSettings.Controls.Add(this.numericUpDownWindowSize);
            this.groupboxViewSettings.Controls.Add(this.checkBoxIntensity);
            this.groupboxViewSettings.Controls.Add(this.checkBoxRawImage);
            this.groupboxViewSettings.Controls.Add(this.radioButtonGraphUnitUm);
            this.groupboxViewSettings.Controls.Add(this.radioButtonGraphUnitMm);
            this.groupboxViewSettings.Controls.Add(this.label7);
            this.groupboxViewSettings.Controls.Add(this.label6);
            this.groupboxViewSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupboxViewSettings.Location = new System.Drawing.Point(267, 12);
            this.groupboxViewSettings.Name = "groupboxViewSettings";
            this.groupboxViewSettings.Size = new System.Drawing.Size(249, 139);
            this.groupboxViewSettings.TabIndex = 5;
            this.groupboxViewSettings.TabStop = false;
            this.groupboxViewSettings.Text = "View Settings";
            // 
            // buttonRefraction
            // 
            this.buttonRefraction.Location = new System.Drawing.Point(155, 107);
            this.buttonRefraction.Name = "buttonRefraction";
            this.buttonRefraction.Size = new System.Drawing.Size(89, 26);
            this.buttonRefraction.TabIndex = 12;
            this.buttonRefraction.Text = "Refraction";
            this.buttonRefraction.UseVisualStyleBackColor = true;
            // 
            // checkBoxThickness
            // 
            this.checkBoxThickness.AutoSize = true;
            this.checkBoxThickness.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.checkBoxThickness.Location = new System.Drawing.Point(9, 114);
            this.checkBoxThickness.Name = "checkBoxThickness";
            this.checkBoxThickness.Size = new System.Drawing.Size(129, 21);
            this.checkBoxThickness.TabIndex = 11;
            this.checkBoxThickness.Text = "Show Thickness";
            this.checkBoxThickness.UseVisualStyleBackColor = true;
            // 
            // numericUpDownWindowSize
            // 
            this.numericUpDownWindowSize.Location = new System.Drawing.Point(183, 21);
            this.numericUpDownWindowSize.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownWindowSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWindowSize.Name = "numericUpDownWindowSize";
            this.numericUpDownWindowSize.Size = new System.Drawing.Size(62, 23);
            this.numericUpDownWindowSize.TabIndex = 10;
            this.numericUpDownWindowSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkBoxIntensity
            // 
            this.checkBoxIntensity.AutoSize = true;
            this.checkBoxIntensity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.checkBoxIntensity.Location = new System.Drawing.Point(9, 70);
            this.checkBoxIntensity.Name = "checkBoxIntensity";
            this.checkBoxIntensity.Size = new System.Drawing.Size(117, 21);
            this.checkBoxIntensity.TabIndex = 9;
            this.checkBoxIntensity.Text = "Show Intensity";
            this.checkBoxIntensity.UseVisualStyleBackColor = true;
            // 
            // checkBoxRawImage
            // 
            this.checkBoxRawImage.AutoSize = true;
            this.checkBoxRawImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.checkBoxRawImage.Location = new System.Drawing.Point(9, 92);
            this.checkBoxRawImage.Name = "checkBoxRawImage";
            this.checkBoxRawImage.Size = new System.Drawing.Size(134, 21);
            this.checkBoxRawImage.TabIndex = 6;
            this.checkBoxRawImage.Text = "Show Raw Image";
            this.checkBoxRawImage.UseVisualStyleBackColor = true;
            // 
            // radioButtonGraphUnitUm
            // 
            this.radioButtonGraphUnitUm.AutoSize = true;
            this.radioButtonGraphUnitUm.Location = new System.Drawing.Point(193, 45);
            this.radioButtonGraphUnitUm.Name = "radioButtonGraphUnitUm";
            this.radioButtonGraphUnitUm.Size = new System.Drawing.Size(45, 21);
            this.radioButtonGraphUnitUm.TabIndex = 8;
            this.radioButtonGraphUnitUm.Text = "µm";
            this.radioButtonGraphUnitUm.UseVisualStyleBackColor = true;
            // 
            // radioButtonGraphUnitMm
            // 
            this.radioButtonGraphUnitMm.AutoSize = true;
            this.radioButtonGraphUnitMm.Checked = true;
            this.radioButtonGraphUnitMm.Location = new System.Drawing.Point(139, 45);
            this.radioButtonGraphUnitMm.Name = "radioButtonGraphUnitMm";
            this.radioButtonGraphUnitMm.Size = new System.Drawing.Size(48, 21);
            this.radioButtonGraphUnitMm.TabIndex = 1;
            this.radioButtonGraphUnitMm.TabStop = true;
            this.radioButtonGraphUnitMm.Text = "mm";
            this.radioButtonGraphUnitMm.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 17);
            this.label7.TabIndex = 7;
            this.label7.Text = "Graph Unit:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Window Height [mm]:";
            // 
            // groupboxSurface
            // 
            this.groupboxSurface.Controls.Add(this.buttonFilter);
            this.groupboxSurface.Controls.Add(this.comboBoxTop);
            this.groupboxSurface.Controls.Add(this.comboBoxBottom);
            this.groupboxSurface.Controls.Add(this.comboBoxBrightest);
            this.groupboxSurface.Controls.Add(this.radioButtonExportAll);
            this.groupboxSurface.Controls.Add(this.buttonExportPeakData);
            this.groupboxSurface.Controls.Add(this.radioButtonExportBrightest);
            this.groupboxSurface.Controls.Add(this.radioButtonExportBottom);
            this.groupboxSurface.Controls.Add(this.radioButtonExportTop);
            this.groupboxSurface.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupboxSurface.Location = new System.Drawing.Point(267, 157);
            this.groupboxSurface.Name = "groupboxSurface";
            this.groupboxSurface.Size = new System.Drawing.Size(249, 118);
            this.groupboxSurface.TabIndex = 7;
            this.groupboxSurface.TabStop = false;
            this.groupboxSurface.Text = "Surface Selection";
            // 
            // buttonFilter
            // 
            this.buttonFilter.Location = new System.Drawing.Point(169, 49);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(75, 26);
            this.buttonFilter.TabIndex = 11;
            this.buttonFilter.Text = "Filter";
            this.buttonFilter.UseVisualStyleBackColor = true;
            // 
            // comboBoxTop
            // 
            this.comboBoxTop.Items.AddRange(new object[] {
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
            this.comboBoxTop.Location = new System.Drawing.Point(117, 66);
            this.comboBoxTop.Name = "comboBoxTop";
            this.comboBoxTop.Size = new System.Drawing.Size(38, 24);
            this.comboBoxTop.TabIndex = 12;
            // 
            // comboBoxBottom
            // 
            this.comboBoxBottom.Items.AddRange(new object[] {
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
            this.comboBoxBottom.Location = new System.Drawing.Point(117, 90);
            this.comboBoxBottom.Name = "comboBoxBottom";
            this.comboBoxBottom.Size = new System.Drawing.Size(38, 24);
            this.comboBoxBottom.TabIndex = 13;
            // 
            // comboBoxBrightest
            // 
            this.comboBoxBrightest.Items.AddRange(new object[] {
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
            this.comboBoxBrightest.Location = new System.Drawing.Point(117, 42);
            this.comboBoxBrightest.Name = "comboBoxBrightest";
            this.comboBoxBrightest.Size = new System.Drawing.Size(38, 24);
            this.comboBoxBrightest.TabIndex = 14;
            // 
            // radioButtonExportAll
            // 
            this.radioButtonExportAll.AutoSize = true;
            this.radioButtonExportAll.Checked = true;
            this.radioButtonExportAll.Location = new System.Drawing.Point(6, 18);
            this.radioButtonExportAll.Name = "radioButtonExportAll";
            this.radioButtonExportAll.Size = new System.Drawing.Size(41, 21);
            this.radioButtonExportAll.TabIndex = 4;
            this.radioButtonExportAll.TabStop = true;
            this.radioButtonExportAll.Text = "All";
            this.radioButtonExportAll.UseVisualStyleBackColor = true;
            // 
            // buttonExportPeakData
            // 
            this.buttonExportPeakData.Location = new System.Drawing.Point(169, 80);
            this.buttonExportPeakData.Name = "buttonExportPeakData";
            this.buttonExportPeakData.Size = new System.Drawing.Size(75, 26);
            this.buttonExportPeakData.TabIndex = 3;
            this.buttonExportPeakData.Text = "Export";
            this.buttonExportPeakData.UseVisualStyleBackColor = true;
            // 
            // radioButtonExportBrightest
            // 
            this.radioButtonExportBrightest.AutoSize = true;
            this.radioButtonExportBrightest.Location = new System.Drawing.Point(6, 43);
            this.radioButtonExportBrightest.Name = "radioButtonExportBrightest";
            this.radioButtonExportBrightest.Size = new System.Drawing.Size(111, 21);
            this.radioButtonExportBrightest.TabIndex = 2;
            this.radioButtonExportBrightest.Text = "Brightest Top";
            this.radioButtonExportBrightest.UseVisualStyleBackColor = true;
            // 
            // radioButtonExportBottom
            // 
            this.radioButtonExportBottom.AutoSize = true;
            this.radioButtonExportBottom.Location = new System.Drawing.Point(6, 93);
            this.radioButtonExportBottom.Name = "radioButtonExportBottom";
            this.radioButtonExportBottom.Size = new System.Drawing.Size(70, 21);
            this.radioButtonExportBottom.TabIndex = 1;
            this.radioButtonExportBottom.Text = "Bottom";
            this.radioButtonExportBottom.UseVisualStyleBackColor = true;
            // 
            // radioButtonExportTop
            // 
            this.radioButtonExportTop.AutoSize = true;
            this.radioButtonExportTop.Location = new System.Drawing.Point(6, 69);
            this.radioButtonExportTop.Name = "radioButtonExportTop";
            this.radioButtonExportTop.Size = new System.Drawing.Size(51, 21);
            this.radioButtonExportTop.TabIndex = 0;
            this.radioButtonExportTop.Text = "Top";
            this.radioButtonExportTop.UseVisualStyleBackColor = true;
            // 
            // groupboxSensorSettings
            // 
            this.groupboxSensorSettings.Controls.Add(this.textBoxMinThickness);
            this.groupboxSensorSettings.Controls.Add(this.label11);
            this.groupboxSensorSettings.Controls.Add(this.label10);
            this.groupboxSensorSettings.Controls.Add(this.comboBoxSensitivity);
            this.groupboxSensorSettings.Controls.Add(this.label9);
            this.groupboxSensorSettings.Controls.Add(this.comboBoxMaterialType);
            this.groupboxSensorSettings.Controls.Add(this.label8);
            this.groupboxSensorSettings.Controls.Add(this.buttonPeakDetection);
            this.groupboxSensorSettings.Controls.Add(this.buttonAdvanced);
            this.groupboxSensorSettings.Controls.Add(this.checkBoxHeightZeroAdjust);
            this.groupboxSensorSettings.Controls.Add(this.textBoxAgcTargetIntensity);
            this.groupboxSensorSettings.Controls.Add(this.label5);
            this.groupboxSensorSettings.Controls.Add(this.checkBoxExternalPulsing);
            this.groupboxSensorSettings.Controls.Add(this.checkBoxAgcEnabled);
            this.groupboxSensorSettings.Controls.Add(this.label1);
            this.groupboxSensorSettings.Controls.Add(this.textBoxAverageIntensity);
            this.groupboxSensorSettings.Controls.Add(this.comboboxFrequency);
            this.groupboxSensorSettings.Controls.Add(this.textBoxMaxPulseWidth);
            this.groupboxSensorSettings.Controls.Add(this.label3);
            this.groupboxSensorSettings.Controls.Add(this.buttonApply);
            this.groupboxSensorSettings.Controls.Add(this.comboboxLedPulseWidth);
            this.groupboxSensorSettings.Controls.Add(this.label4);
            this.groupboxSensorSettings.Controls.Add(this.label2);
            this.groupboxSensorSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupboxSensorSettings.Location = new System.Drawing.Point(12, 12);
            this.groupboxSensorSettings.Name = "groupboxSensorSettings";
            this.groupboxSensorSettings.Size = new System.Drawing.Size(249, 391);
            this.groupboxSensorSettings.TabIndex = 6;
            this.groupboxSensorSettings.TabStop = false;
            this.groupboxSensorSettings.Text = "Sensor Settings";
            // 
            // textBoxMinThickness
            // 
            this.textBoxMinThickness.Location = new System.Drawing.Point(183, 278);
            this.textBoxMinThickness.Name = "textBoxMinThickness";
            this.textBoxMinThickness.Size = new System.Drawing.Size(62, 23);
            this.textBoxMinThickness.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 281);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(162, 17);
            this.label11.TabIndex = 23;
            this.label11.Text = "Minimum Thickness [µm]";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 252);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 17);
            this.label10.TabIndex = 22;
            this.label10.Text = "Sensitivity";
            // 
            // comboBoxSensitivity
            // 
            this.comboBoxSensitivity.FormattingEnabled = true;
            this.comboBoxSensitivity.Items.AddRange(new object[] {
            "Custom",
            "Best Accuracy",
            "Best Sensitivity"});
            this.comboBoxSensitivity.Location = new System.Drawing.Point(113, 250);
            this.comboBoxSensitivity.Name = "comboBoxSensitivity";
            this.comboBoxSensitivity.Size = new System.Drawing.Size(132, 24);
            this.comboBoxSensitivity.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 225);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 17);
            this.label9.TabIndex = 20;
            this.label9.Text = "Material Type";
            // 
            // comboBoxMaterialType
            // 
            this.comboBoxMaterialType.FormattingEnabled = true;
            this.comboBoxMaterialType.Items.AddRange(new object[] {
            "Custom",
            "Opaque",
            "Mirror",
            "Transparent"});
            this.comboBoxMaterialType.Location = new System.Drawing.Point(113, 222);
            this.comboBoxMaterialType.Name = "comboBoxMaterialType";
            this.comboBoxMaterialType.Size = new System.Drawing.Size(132, 24);
            this.comboBoxMaterialType.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 202);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 17);
            this.label8.TabIndex = 18;
            this.label8.Text = "Peak Detection";
            // 
            // buttonPeakDetection
            // 
            this.buttonPeakDetection.Location = new System.Drawing.Point(163, 306);
            this.buttonPeakDetection.Name = "buttonPeakDetection";
            this.buttonPeakDetection.Size = new System.Drawing.Size(82, 26);
            this.buttonPeakDetection.TabIndex = 17;
            this.buttonPeakDetection.Text = "Custom";
            this.buttonPeakDetection.UseVisualStyleBackColor = true;
            // 
            // buttonAdvanced
            // 
            this.buttonAdvanced.Location = new System.Drawing.Point(75, 357);
            this.buttonAdvanced.Name = "buttonAdvanced";
            this.buttonAdvanced.Size = new System.Drawing.Size(82, 26);
            this.buttonAdvanced.TabIndex = 16;
            this.buttonAdvanced.Text = "Advanced";
            this.buttonAdvanced.UseVisualStyleBackColor = true;
            // 
            // checkBoxHeightZeroAdjust
            // 
            this.checkBoxHeightZeroAdjust.AutoSize = true;
            this.checkBoxHeightZeroAdjust.Location = new System.Drawing.Point(9, 333);
            this.checkBoxHeightZeroAdjust.Name = "checkBoxHeightZeroAdjust";
            this.checkBoxHeightZeroAdjust.Size = new System.Drawing.Size(176, 21);
            this.checkBoxHeightZeroAdjust.TabIndex = 9;
            this.checkBoxHeightZeroAdjust.Text = "Height Zero Adjustment";
            this.checkBoxHeightZeroAdjust.UseVisualStyleBackColor = true;
            // 
            // textBoxAgcTargetIntensity
            // 
            this.textBoxAgcTargetIntensity.Location = new System.Drawing.Point(183, 76);
            this.textBoxAgcTargetIntensity.Name = "textBoxAgcTargetIntensity";
            this.textBoxAgcTargetIntensity.Size = new System.Drawing.Size(62, 23);
            this.textBoxAgcTargetIntensity.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "AGC Target Intensity:";
            // 
            // checkBoxExternalPulsing
            // 
            this.checkBoxExternalPulsing.AutoSize = true;
            this.checkBoxExternalPulsing.Location = new System.Drawing.Point(9, 152);
            this.checkBoxExternalPulsing.Name = "checkBoxExternalPulsing";
            this.checkBoxExternalPulsing.Size = new System.Drawing.Size(184, 21);
            this.checkBoxExternalPulsing.TabIndex = 7;
            this.checkBoxExternalPulsing.Text = "External Pulsing Enabled";
            this.checkBoxExternalPulsing.UseVisualStyleBackColor = true;
            // 
            // checkBoxAgcEnabled
            // 
            this.checkBoxAgcEnabled.AutoSize = true;
            this.checkBoxAgcEnabled.Location = new System.Drawing.Point(9, 58);
            this.checkBoxAgcEnabled.Name = "checkBoxAgcEnabled";
            this.checkBoxAgcEnabled.Size = new System.Drawing.Size(112, 21);
            this.checkBoxAgcEnabled.TabIndex = 6;
            this.checkBoxAgcEnabled.Text = "AGC Enabled";
            this.checkBoxAgcEnabled.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Average Intensity:";
            // 
            // textBoxAverageIntensity
            // 
            this.textBoxAverageIntensity.Location = new System.Drawing.Point(9, 33);
            this.textBoxAverageIntensity.Name = "textBoxAverageIntensity";
            this.textBoxAverageIntensity.ReadOnly = true;
            this.textBoxAverageIntensity.Size = new System.Drawing.Size(191, 23);
            this.textBoxAverageIntensity.TabIndex = 3;
            // 
            // comboboxFrequency
            // 
            this.comboboxFrequency.FormattingEnabled = true;
            this.comboboxFrequency.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "100",
            "200",
            "300",
            "500",
            "700",
            "1000",
            "2000",
            "3000",
            "5000"});
            this.comboboxFrequency.Location = new System.Drawing.Point(183, 174);
            this.comboboxFrequency.Name = "comboboxFrequency";
            this.comboboxFrequency.Size = new System.Drawing.Size(62, 24);
            this.comboboxFrequency.TabIndex = 2;
            // 
            // textBoxMaxPulseWidth
            // 
            this.textBoxMaxPulseWidth.Location = new System.Drawing.Point(183, 127);
            this.textBoxMaxPulseWidth.Name = "textBoxMaxPulseWidth";
            this.textBoxMaxPulseWidth.Size = new System.Drawing.Size(62, 23);
            this.textBoxMaxPulseWidth.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Max. LED Pulse Width [µs]";
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(163, 357);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(82, 26);
            this.buttonApply.TabIndex = 0;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            // 
            // comboboxLedPulseWidth
            // 
            this.comboboxLedPulseWidth.FormattingEnabled = true;
            this.comboboxLedPulseWidth.Items.AddRange(new object[] {
            "5.0",
            "6.0",
            "7.0",
            "8.0",
            "9.0",
            "10.0",
            "15.0",
            "20.0",
            "25.0",
            "30.0",
            "40.0",
            "50.0",
            "60.0",
            "70.0",
            "80.0",
            "90.0",
            "100.0",
            "110.0",
            "120.0",
            "130.0",
            "140.0",
            "150.0",
            "160.0",
            "170.0",
            "180.0",
            "190.0",
            "200.0",
            "250.0",
            "300.0",
            "350.0",
            "400.0",
            "450.0",
            "500.0",
            "550.0",
            "600.0",
            "650.0",
            "700.0",
            "750.0",
            "800.0",
            "850.0",
            "900.0",
            "950.0",
            "1000.0"});
            this.comboboxLedPulseWidth.Location = new System.Drawing.Point(183, 101);
            this.comboboxLedPulseWidth.Name = "comboboxLedPulseWidth";
            this.comboboxLedPulseWidth.Size = new System.Drawing.Size(62, 24);
            this.comboboxLedPulseWidth.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "LED Pulse Width [µs]:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Frequency [Hz]:";
            // 
            // _batchMode
            // 
            this._batchMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._batchMode.IsBatchVisualizerVisible = false;
            this._batchMode.IsConfigured = false;
            this._batchMode.Location = new System.Drawing.Point(268, 283);
            this._batchMode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._batchMode.Name = "_batchMode";
            this._batchMode.Size = new System.Drawing.Size(249, 242);
            this._batchMode.TabIndex = 8;
            // 
            // SensorSettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 535);
            this.Controls.Add(this._batchMode);
            this.Controls.Add(this.groupboxViewSettings);
            this.Controls.Add(this.groupboxSurface);
            this.Controls.Add(this.groupboxSensorSettings);
            this.Name = "SensorSettingsView";
            this.Text = "Sensor Settings";
            this.groupboxViewSettings.ResumeLayout(false);
            this.groupboxViewSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindowSize)).EndInit();
            this.groupboxSurface.ResumeLayout(false);
            this.groupboxSurface.PerformLayout();
            this.groupboxSensorSettings.ResumeLayout(false);
            this.groupboxSensorSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox groupboxViewSettings;
        public System.Windows.Forms.Button buttonRefraction;
        public System.Windows.Forms.CheckBox checkBoxThickness;
        public System.Windows.Forms.NumericUpDown numericUpDownWindowSize;
        public System.Windows.Forms.CheckBox checkBoxIntensity;
        public System.Windows.Forms.CheckBox checkBoxRawImage;
        public System.Windows.Forms.RadioButton radioButtonGraphUnitUm;
        public System.Windows.Forms.RadioButton radioButtonGraphUnitMm;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.GroupBox groupboxSurface;
        public System.Windows.Forms.Button buttonFilter;
        public System.Windows.Forms.ComboBox comboBoxTop;
        public System.Windows.Forms.ComboBox comboBoxBottom;
        public System.Windows.Forms.ComboBox comboBoxBrightest;
        public System.Windows.Forms.RadioButton radioButtonExportAll;
        public System.Windows.Forms.Button buttonExportPeakData;
        public System.Windows.Forms.RadioButton radioButtonExportBrightest;
        public System.Windows.Forms.RadioButton radioButtonExportBottom;
        public System.Windows.Forms.RadioButton radioButtonExportTop;
        public System.Windows.Forms.GroupBox groupboxSensorSettings;
        public System.Windows.Forms.TextBox textBoxMinThickness;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.ComboBox comboBoxSensitivity;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.ComboBox comboBoxMaterialType;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.Button buttonPeakDetection;
        public System.Windows.Forms.Button buttonAdvanced;
        public System.Windows.Forms.CheckBox checkBoxHeightZeroAdjust;
        public System.Windows.Forms.TextBox textBoxAgcTargetIntensity;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.CheckBox checkBoxExternalPulsing;
        public System.Windows.Forms.CheckBox checkBoxAgcEnabled;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxAverageIntensity;
        public System.Windows.Forms.ComboBox comboboxFrequency;
        public System.Windows.Forms.TextBox textBoxMaxPulseWidth;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button buttonApply;
        public System.Windows.Forms.ComboBox comboboxLedPulseWidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        public BatchModePresenter _batchMode;
    }
}