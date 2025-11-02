using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FocalSpec.GuiExample.View
{
    public partial class SensorSettingsView : Form
    {
        private MainView _mainView;

        // Default constructor for the designer view (it avoids it freaking out)
        public SensorSettingsView()
        {
            InitializeComponent();
        }
        // Constructor used by MainView
        public SensorSettingsView(MainView mainView)
        {
            _mainView = mainView;
            InitializeComponent();
            WireUpEvents();
        }

        private void SensorSettingsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;   // stop it from closing
            this.Hide();       // just hide instead
        }

        /* When moving buttons from MainView to this settings menu, the events and their heavily entrenched logic did not carry over.
          This function wires events back up by using a reference to main*/
        private void WireUpEvents()
        {
            this.buttonRefraction.Click += new System.EventHandler(_mainView._buttonRefraction_Click);
            this.checkBoxThickness.CheckedChanged += new System.EventHandler(_mainView.checkBoxThickness_CheckedChanged);
            this.numericUpDownWindowSize.ValueChanged += new System.EventHandler(_mainView.numericUpDownWindowSize_ValueChanged);
            this.checkBoxIntensity.CheckedChanged += new System.EventHandler(_mainView.checkBoxIntensity_CheckedChanged);
            this.checkBoxRawImage.CheckedChanged += new System.EventHandler(_mainView.checkBoxRawImage_CheckedChanged);
            this.radioButtonGraphUnitUm.CheckedChanged += new System.EventHandler(_mainView.graphUnitUm_CheckedChanged);
            this.radioButtonGraphUnitMm.CheckedChanged += new System.EventHandler(_mainView.graphUnitMm_CheckedChanged);
            this.buttonFilter.Click += new System.EventHandler(_mainView._buttonFilter_Click);
            this.comboBoxTop.SelectedIndexChanged += new System.EventHandler(_mainView.ComboBoxTop_SelectedIndexChanged);
            this.comboBoxBottom.SelectedIndexChanged += new System.EventHandler(_mainView.ComboBoxBottom_SelectedIndexChanged);
            this.comboBoxBrightest.SelectedIndexChanged += new System.EventHandler(_mainView.ComboBoxBrightest_SelectedIndexChanged);
            this.radioButtonExportAll.CheckedChanged += new System.EventHandler(_mainView.RadioButtonProfileLayer_CheckedChanged);
            this.buttonExportPeakData.Click += new System.EventHandler(_mainView._exportPeakData_Click);
            this.radioButtonExportBrightest.CheckedChanged += new System.EventHandler(_mainView.RadioButtonProfileLayer_CheckedChanged);
            this.radioButtonExportBottom.CheckedChanged += new System.EventHandler(_mainView.RadioButtonProfileLayer_CheckedChanged);
            this.radioButtonExportTop.CheckedChanged += new System.EventHandler(_mainView.RadioButtonProfileLayer_CheckedChanged);
            this.textBoxMinThickness.TextChanged += new System.EventHandler(_mainView.textBoxMinThickness_TextChanged);
            this.comboBoxSensitivity.SelectedIndexChanged += new System.EventHandler(_mainView.ComboBoxSensitivity_SelectedIndexChanged);
            this.comboBoxMaterialType.SelectedIndexChanged += new System.EventHandler(_mainView.ComboBoxMaterialType_SelectedIndexChanged);
            this.buttonPeakDetection.Click += new System.EventHandler(_mainView.ButtonPeakDetection_Click);
            this.checkBoxHeightZeroAdjust.CheckedChanged += new System.EventHandler(_mainView.checkBoxHeightZeroAdjust_CheckedChanged);
            this.checkBoxAgcEnabled.CheckedChanged += new System.EventHandler(_mainView.CheckBoxAgcEnabled_CheckedChanged);
            this.buttonApply.Click += new System.EventHandler(_mainView.buttonApply_Click);
            this.comboboxLedPulseWidth.Enter += new System.EventHandler(_mainView.ComboboxLedPulseWidth_Enter);
            this.buttonAdvanced.Click += new System.EventHandler(_mainView._buttonAdvanced_Click);
        }
    }
}
