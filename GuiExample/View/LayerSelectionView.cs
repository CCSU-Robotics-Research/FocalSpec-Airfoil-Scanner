using System.Windows.Forms;

namespace FocalSpec.GuiExample.View
{
    public partial class LayerSelectionView : Form
    {
        public int Layer;

        public LayerSelectionView(int maxLayer)
        {
            InitializeComponent();
            numericUpDownLayer.Maximum = maxLayer + 1;
        }

        private void buttonOk_Click(object sender, System.EventArgs e)
        {
            Layer = (int)numericUpDownLayer.Value - 1;
            Close();
        }
    }
}
