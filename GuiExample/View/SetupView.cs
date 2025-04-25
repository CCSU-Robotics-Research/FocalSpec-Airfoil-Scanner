using System;
using System.Windows.Forms;
using FocalSpec.GuiExample.Model;

namespace FocalSpec.GuiExample.View
{
    public partial class SetupView : Form, ISetupView
    {
        public SetupView()
        {
            InitializeComponent();
        }

        public string ZCalibrationFilePath { get; set; }

        public string XCalibrationFilePath { get; set; }

        public bool IsZCalibrationFileSet
        {
            get { return ZCalibrationFilePath != null; }
        }

        public bool IsXCalibrationFileSet
        {
            get { return XCalibrationFilePath != null; }
        }

        public bool ShowModal()
        {
            if (ZCalibrationFilePath != null)
                TextBoxZCalibrationFile.Text = ZCalibrationFilePath;
            if (XCalibrationFilePath != null)
                TextBoxXCalibrationFile.Text = XCalibrationFilePath;

            return ShowDialog() == DialogResult.OK;
        }

        private void Reset()
        {
            ZCalibrationFilePath = null;
            XCalibrationFilePath = null;
        }

        private void ButtonZCalibrationFile_Click(object sender, EventArgs e)
        {
            string path;
            if (TryGetFile("Select Z calibration file", out path))
            {
                ZCalibrationFilePath = TextBoxZCalibrationFile.Text = path;
                ButtonOk.Enabled = IsSetupComplete();
            }
        }

        private void ButtonXCalibrationFile_Click(object sender, EventArgs e)
        {
            string path;
            if (TryGetFile("Select X calibration file", out path))
            {
                XCalibrationFilePath = TextBoxXCalibrationFile.Text = path;
                ButtonOk.Enabled = IsSetupComplete();
            }
        }

        private bool IsSetupComplete()
        {
            return IsZCalibrationFileSet && IsXCalibrationFileSet;
        }

        private bool TryGetFile(string title, out string file)
        {
            file = null;

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                DefaultExt = Defines.CalibrationFileExt,
                Filter = string.Format("Calibration file (*{0})|*{0}", Defines.CalibrationFileExt),
                Title = title
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog.FileName;
                return true;
            }
            
            return false;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
//            Reset();
            Close();
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
