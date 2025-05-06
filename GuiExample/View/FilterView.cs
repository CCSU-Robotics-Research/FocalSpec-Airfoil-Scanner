using System;
using System.Globalization;
using System.Windows.Forms;

namespace FocalSpec.GuiExample.View
{
    public partial class FilterView : Form
    {
        public bool IsNoiseRemoval { get; set; }
        public bool PeakXFilterEnabled { get; set; }
        public bool IsTrimEdges { get; set; }
        public double AverageZ { get; set; }
        public double AverageIntensity { get; set; }
        public int MedianZ { get; set; }
        public int MedianIntensity { get; set; }
        public double Resample { get; set; }
        public int PeakXFilter { get; set; }
        public double FillGapMax { get; set; } = 0;

        public bool ApplyFilters { get; set; }

        public FilterView()
        {
            InitializeComponent();
        }

        private void FilterView_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void FilterView_Shown(object sender, EventArgs e)
        {
            checkBoxNoiseRemoval.Checked = IsNoiseRemoval;
            checkBoxAverageZ.Checked = Math.Abs(AverageZ) > double.Epsilon;
            checkBoxAverageIntensity.Checked = Math.Abs(AverageIntensity) > double.Epsilon;
            checkBoxMedianZ.Checked = MedianZ != 0;
            checkBoxMedianIntensity.Checked = MedianIntensity != 0;
            checkBoxResample.Checked = Math.Abs(Resample) > double.Epsilon;
            checkBoxPeakXfilter.Checked = PeakXFilter != 0;
            checkBoxFillGapMax.Checked = Math.Abs(FillGapMax) > double.Epsilon;
            checkBoxTrimEdges.Checked = IsTrimEdges;
            textBoxAverageZ.Text = AverageZ.ToString(CultureInfo.InvariantCulture);
            textBoxAverageIntensity.Text = AverageIntensity.ToString(CultureInfo.InvariantCulture);
            textBoxMedianZ.Text = MedianZ.ToString(CultureInfo.InvariantCulture);
            textBoxMedianIntensity.Text = MedianIntensity.ToString(CultureInfo.InvariantCulture);
            textBoxResample.Text = Resample.ToString(CultureInfo.InvariantCulture);
            comboBoxPeakXFilter.Text = comboBoxPeakXFilter.Items.Contains(PeakXFilter.ToString()) ? PeakXFilter.ToString() : "7";
            textBoxFillGapMax.Text = FillGapMax.ToString(CultureInfo.InvariantCulture);
            EnableControls();
        }

        private void checkBoxAverageZ_CheckedChanged(object sender, EventArgs e)
        {
            EnableControls();
        }

        private void checkBoxAverageIntensity_CheckedChanged(object sender, EventArgs e)
        {
            EnableControls();
        }

        private void checkBoxMedianZ_CheckedChanged(object sender, EventArgs e)
        {
            EnableControls();
        }

        private void checkBoxMedianIntensity_CheckedChanged(object sender, EventArgs e)
        {
            EnableControls();
        }

        private void checkBoxResample_CheckedChanged(object sender, EventArgs e)
        {
            EnableControls();
        }

	    private void checkBoxPeakXFilter_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox) sender;
            if (checkbox.Checked && PeakXFilter == 0)
            {
                const string filter = "7";
                comboBoxPeakXFilter.Text = filter;
            }

            EnableControls();
	    }

        private void checkBoxFillGapMax_CheckedChanged(object sender, EventArgs e)
        {
            EnableControls();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
	        ApplyFilters = false;
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            IsNoiseRemoval = checkBoxNoiseRemoval.Checked;
            IsTrimEdges = checkBoxTrimEdges.Checked;
            try
            {
                AverageZ = checkBoxAverageZ.Checked ? double.Parse(textBoxAverageZ.Text, CultureInfo.InvariantCulture) : 0;
                AverageIntensity = checkBoxAverageIntensity.Checked ? double.Parse(textBoxAverageIntensity.Text, CultureInfo.InvariantCulture) : 0;
                MedianZ = checkBoxMedianZ.Checked ? int.Parse(textBoxMedianZ.Text, CultureInfo.InvariantCulture) : 0;
                MedianIntensity = checkBoxMedianIntensity.Checked ? int.Parse(textBoxMedianIntensity.Text, CultureInfo.InvariantCulture) : 0;
                Resample = checkBoxResample.Checked ? double.Parse(textBoxResample.Text, CultureInfo.InvariantCulture) : 0;
	            PeakXFilter = checkBoxPeakXfilter.Checked ? int.Parse(comboBoxPeakXFilter.Text, CultureInfo.InvariantCulture) : 0;         
                FillGapMax = checkBoxFillGapMax.Checked ? double.Parse(textBoxFillGapMax.Text, CultureInfo.InvariantCulture) : 0;
            }
            catch (Exception)
            {
                const string message = "Incorrect filter values. Please, set correct values.";
                MessageBox.Show(message);
                return;
            }
	        ApplyFilters = true;
            Close();
        }

        private void EnableControls()
        {
            checkBoxPeakXfilter.Enabled = PeakXFilterEnabled;

            textBoxAverageZ.Enabled = checkBoxAverageZ.Checked;
            textBoxAverageIntensity.Enabled = checkBoxAverageIntensity.Checked;
            textBoxMedianZ.Enabled = checkBoxMedianZ.Checked;
            textBoxMedianIntensity.Enabled = checkBoxMedianIntensity.Checked;
            textBoxResample.Enabled = checkBoxResample.Checked;
            comboBoxPeakXFilter.Enabled = checkBoxPeakXfilter.Checked;
            textBoxFillGapMax.Enabled = checkBoxFillGapMax.Checked;
        }

        private void textBoxResample_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                var value = double.Parse(textBoxResample.Text, CultureInfo.InvariantCulture);
                if (value > 0 && value < 2)
                {
                    const string message = "Minimum value for Resample X-Resolution is 2 µm.";
                    MessageBox.Show(message);
                    textBoxResample.Text = @"0";
                }
            }
            catch (Exception)
            {
                textBoxResample.Text = @"0";
            }
        }
    }
}
