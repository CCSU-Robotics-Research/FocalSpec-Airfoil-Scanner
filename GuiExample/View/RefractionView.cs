using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using FocalSpec.GuiExample.Model.Camera;

namespace FocalSpec.GuiExample.View
{
    public partial class RefractionView : Form
    {
        public List<float> Indexes = new List<float>();

        private int _lastLayer;

        public RefractionView()
        {
            InitializeComponent();
            
            Reset();
        }

        public void Reset()
        {
            Indexes.Clear();
            for (int i = 0; i < SensorParameterStore.GetInstance().Layers.Length; i++)
                Indexes.Add(1.0f);
        }

        private void RefractionView_Shown(object sender, EventArgs e)
        {
            _lastLayer = 0;
            textBoxRefractiveIndex.Text = Indexes[0].ToString(CultureInfo.InvariantCulture);
            comboBoxLayer.SelectedIndex = 0;
        }

        private void RefractionView_FormClosing(object sender, FormClosingEventArgs e)
        {
            StoreIndex();
        }

        private void ComboBoxLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            StoreIndex();
            if (comboBoxLayer.SelectedIndex < 0 || comboBoxLayer.SelectedIndex >= Indexes.Count) return;
            textBoxRefractiveIndex.Text = Indexes[comboBoxLayer.SelectedIndex].ToString(CultureInfo.InvariantCulture);
            _lastLayer = comboBoxLayer.SelectedIndex;
        }

        private void StoreIndex()
        {
            try
            {
                Indexes[_lastLayer] = float.Parse(textBoxRefractiveIndex.Text, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                Indexes[_lastLayer] = 1.0f;
            }
        }
    }
}
