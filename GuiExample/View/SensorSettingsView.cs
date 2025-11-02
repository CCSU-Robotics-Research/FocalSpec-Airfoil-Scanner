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
        public SensorSettingsView(MainView mainView)
        {
            _mainView = mainView;
            InitializeComponent();
        }

        private void SensorSettingsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;   // stop it from closing
            this.Hide();       // just hide instead
        }
    }
}
