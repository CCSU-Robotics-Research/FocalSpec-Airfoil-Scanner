using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using FocalSpec.GuiExample.Model.Camera;

namespace FocalSpec.GuiExample.View
{
    public partial class AdvancedView : Form
    {
        public struct LayerSetting
        {
            public LayerSetting(int layer, double thickness, int intensityType)
            {
                Layer = layer;
                Thickness = thickness;
                IntensityType = intensityType;
            }
            public int Layer;
            public double Thickness;
            public int IntensityType;
        }

        public bool IsHs { get; set; }
        public bool IsHdrEnabled { get; set; }
        public bool LayerIntensityTypeEnabled { get; set; }
        public bool Hdr { get; set; }
        public float VLow2 { get; set; }
        public float VLow3 { get; set; }
        public float Kp1Pos { get; set; }
        public float Kp2Pos { get; set; }
        public List<LayerSetting> LayerSettings = new List<LayerSetting>();

        private int _lastLayer;
        private bool _visible;

        public AdvancedView()
        {
            InitializeComponent();

            for (int i = 0; i < SensorParameterStore.GetInstance().Layers.Length; i++)
                LayerSettings.Add(new LayerSetting(i, 0, 0));
        }

        private void EnableControls()
        {
            checkBoxHdrEnabled.Enabled = IsHdrEnabled;
            CheckBoxLayerParameters.Enabled = LayerIntensityTypeEnabled;
            numericUpDownVLow2.Enabled = checkBoxHdrEnabled.Checked;
            numericUpDownVLow3.Enabled = checkBoxHdrEnabled.Checked;
            numericUpDownKp1Pos.Enabled = checkBoxHdrEnabled.Checked;
            numericUpDownKp2Pos.Enabled = checkBoxHdrEnabled.Checked;
            comboBoxLayer.Enabled = CheckBoxLayerParameters.Checked;
            textBoxThickness.Enabled = CheckBoxLayerParameters.Checked;
            comboBoxIntensityType.Enabled = CheckBoxLayerParameters.Checked;
        }

        private void SetLayerSettings()
        {
            var setting = LayerSettings[_lastLayer];
            try
            {
                setting.Thickness = double.Parse(textBoxThickness.Text, CultureInfo.InvariantCulture);
                if (setting.Thickness < 0)
                    setting.Thickness = 0;
            }
            catch (Exception)
            {
                setting.Thickness = 0;
            }
            setting.IntensityType = comboBoxIntensityType.SelectedIndex >= 0 ? comboBoxIntensityType.SelectedIndex : 0;
            LayerSettings[_lastLayer] = setting;
        }

        private void ShowLayerSettings()
        {
            var setting = LayerSettings[comboBoxLayer.SelectedIndex];
            textBoxThickness.Text = setting.Thickness.ToString(CultureInfo.InvariantCulture);
            comboBoxIntensityType.SelectedIndex = setting.IntensityType;
        }

        private void AdvancedView_Shown(object sender, EventArgs e)
        {
            buttonOk.Location = new Point(296, 136);
            checkBoxHdrEnabled.Checked = Hdr;

            if (!LayerIntensityTypeEnabled)
            {
                // Hide unnecessary values and shrink the window
                CheckBoxLayerParameters.Visible = false;
                labelLayer.Visible = false;
                comboBoxLayer.Visible = false;
                labelThickness.Visible = false;
                textBoxThickness.Visible = false;
                labelIntensityType.Visible = false;
                comboBoxIntensityType.Visible = false;
                buttonOk.Location = new Point(numericUpDownKp1Pos.Right - buttonOk.Size.Width, buttonOk.Location.Y);
                Width = buttonOk.Right + 30;
            }

            if (IsHs)
            {
                // Hide unnecessary values and shrink the window
                TextVLow3.Visible = false;
                TextKp2Pos.Visible = false;
                numericUpDownVLow3.Visible = false;
                numericUpDownKp2Pos.Visible = false;
                TextKp1Pos.Location = new Point(TextVLow3.Location.X, TextVLow3.Location.Y);
                numericUpDownKp1Pos.Location = new Point(numericUpDownVLow3.Location.X, numericUpDownVLow3.Location.Y);
                if (!LayerIntensityTypeEnabled)
                {
                    buttonOk.Location = new Point(buttonOk.Location.X, TextKp1Pos.Location.Y + 30);
                    Height = buttonOk.Bottom + 50;
                }
            }
            else
            {
                numericUpDownVLow3.Value = (decimal)VLow3;
                numericUpDownKp2Pos.Value = (decimal)Kp2Pos;
            }

            numericUpDownVLow2.Minimum = IsHs ? 30 : 64;
            numericUpDownVLow2.Maximum = IsHs ? 33 : 127;
            numericUpDownVLow2.Value = Math.Max(Math.Min((decimal)VLow2, numericUpDownVLow2.Maximum), numericUpDownVLow2.Minimum);
            try
            {
                numericUpDownKp1Pos.Maximum = IsHs ? (decimal)99.99 : 99;
                numericUpDownKp1Pos.Value = (decimal)Kp1Pos;
            }
            catch (ArgumentOutOfRangeException)
            {
                numericUpDownKp1Pos.Value = 99;
            }

            _lastLayer = 0;
            comboBoxLayer.SelectedIndex = 0;
            textBoxThickness.Text = "";
            comboBoxIntensityType.SelectedIndex = 0;
            _visible = true;
            ShowLayerSettings();

            EnableControls();
        }

        private void AdvancedView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hdr = checkBoxHdrEnabled.Checked;
            VLow2 = (float)numericUpDownVLow2.Value;
            VLow3 = (float)numericUpDownVLow3.Value;
            Kp1Pos = (float)numericUpDownKp1Pos.Value;
            Kp2Pos = (float)numericUpDownKp2Pos.Value;

            LayerIntensityTypeEnabled = CheckBoxLayerParameters.Checked;
            SetLayerSettings();
            _visible = false;
        }

        private void checkBoxHdrEnabled_Click(object sender, EventArgs e)
        {
            EnableControls();
        }

        private void CheckBoxLayerParameters_CheckedChanged(object sender, EventArgs e)
        {
            EnableControls();
        }

        private void ComboBoxLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_visible) return;
            SetLayerSettings();
            ShowLayerSettings();
            _lastLayer = comboBoxLayer.SelectedIndex;
        }
    }
}
