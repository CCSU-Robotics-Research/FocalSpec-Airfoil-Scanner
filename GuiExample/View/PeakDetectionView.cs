using System;
using System.Windows.Forms;

namespace FocalSpec.GuiExample.View
{
    public partial class PeakDetectionView : Form
    {
        public int FirLength;
        public int AverFirLength;
        public int DetectionFilter;
        public int AverageIntensityFilter;
        public int Threshold;

        private bool _isPeakDetection;

        public bool ValuesChanged { get; set; }

        public PeakDetectionView()
        {
            InitializeComponent();

            comboBoxFirLength.SelectedIndex = -1;
            comboBoxAveragingFirLength.SelectedIndex = -1;
            comboBoxDetectionFilter.SelectedIndex = -1;
            comboBoxAverageIntensityFilter.SelectedIndex = -1;
        }

        public bool IsPeakDetection
        {
            get => _isPeakDetection;
            set
            {
                _isPeakDetection = value;
                labelAverageFir.Visible = !_isPeakDetection;
                labelDetectionFilter.Visible = _isPeakDetection;
                labelAverageIntensityFilter.Visible = _isPeakDetection;
                comboBoxAveragingFirLength.Visible = !_isPeakDetection;
                comboBoxDetectionFilter.Visible = _isPeakDetection;
                comboBoxAverageIntensityFilter.Visible = _isPeakDetection;
            }
        }

        public void EnableAveragingFir(bool enable)
        {
            comboBoxAveragingFirLength.Enabled = enable;
        }

        private void PeakDetectionView_Shown(object sender, EventArgs e)
        {
            comboBoxFirLength.Text = FirLength.ToString();
            comboBoxAveragingFirLength.Text = AverFirLength.ToString();
            comboBoxDetectionFilter.Text = DetectionFilter.ToString();
            comboBoxAverageIntensityFilter.Text = AverageIntensityFilter.ToString();
            numericUpDownPeakCoreThreshold.Value = Threshold;

            ValuesChanged = false;
        }

        private void PeakDetectionView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel)
            {
                ValuesChanged = false;
                return;
            }

            var firLength = Convert.ToInt32(comboBoxFirLength.Text);
            var averFirLength = IsPeakDetection ? -1 : Convert.ToInt32(comboBoxAveragingFirLength.Text);
            var detectionFilter = IsPeakDetection ? Convert.ToInt32(comboBoxDetectionFilter.Text) : -1;
            var averageIntensityFilter = IsPeakDetection ? Convert.ToInt32(comboBoxAverageIntensityFilter.Text) : -1;
            var threshold = (int)numericUpDownPeakCoreThreshold.Value;

            if (firLength != FirLength || averFirLength != AverFirLength || detectionFilter != DetectionFilter ||
                averageIntensityFilter != AverageIntensityFilter || threshold != Threshold)
            {
                FirLength = firLength;
                AverFirLength = averFirLength;
                DetectionFilter = detectionFilter;
                AverageIntensityFilter = averageIntensityFilter;
                Threshold = threshold;
                ValuesChanged = true;
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
        }
    }
}
