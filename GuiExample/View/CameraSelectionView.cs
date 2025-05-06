using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FocalSpec.GuiExample.Model;

namespace FocalSpec.GuiExample.View
{
    public partial class CameraSelectionView : Form
    {
        public string Camera;

        private readonly ApplicationSettings _applicationSettings;

        private Dictionary<string, string> _names = new Dictionary<string, string>();

        public CameraSelectionView(List<string> cameraIds, ApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;

            InitializeComponent();

            _names.Clear();
            var storedNames = _applicationSettings.CameraIds;

            foreach (var id in cameraIds)
            {
                _names.Add(id, storedNames.ContainsKey(id) ? storedNames[id] : "");
                listBoxCameras.Items.Add(_names[id] + " (" + id + ")");
            }

            listBoxCameras.SelectedIndex = 0;
        }

        private void CameraSelectionView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Camera = labelMAC.Text;
            _applicationSettings.CameraIds = _names;
        }

        private void listBoxCameras_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (listBoxCameras.SelectedIndex < 0) return;
            labelMAC.Text = _names.Keys.ElementAt(listBoxCameras.SelectedIndex);
            textBoxName.Text = _names.Values.ElementAt(listBoxCameras.SelectedIndex);
        }

        private void textBoxName_TextChanged(object sender, System.EventArgs e)
        {
            var index = listBoxCameras.SelectedIndex;
            _names[labelMAC.Text] = textBoxName.Text;
            listBoxCameras.Items[listBoxCameras.SelectedIndex] = textBoxName.Text + " (" + labelMAC.Text + ")";
            listBoxCameras.SelectedIndex = index;
        }
    }
}
