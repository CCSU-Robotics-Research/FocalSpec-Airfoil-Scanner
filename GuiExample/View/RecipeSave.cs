using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FocalSpec.GuiExample.Model;

namespace FocalSpec.GuiExample.View
{
    public partial class RecipeSave : Form
    {
        public string Recipe;

        private readonly List<string> _files = new List<string>();

        public RecipeSave(string recipe)
        {
            Recipe = recipe;

            InitializeComponent();

            textBoxRecipe.Text = Recipe;

            if (Directory.Exists(Defines.RecipeFolder))
                _files = Directory.GetFiles(Defines.RecipeFolder).Select(Path.GetFileNameWithoutExtension).ToList();
        }

        private void RecipeSave_Load(object sender, System.EventArgs e)
        {
            EnableControls();
        }

        private void buttonOk_Click(object sender, System.EventArgs e)
        {
            Recipe = textBoxRecipe.Text;
        }

        private void textBoxRecipe_TextChanged(object sender, System.EventArgs e)
        {
            EnableControls();
        }

        private void EnableControls()
        {
            buttonOk.Enabled = !string.IsNullOrEmpty(textBoxRecipe.Text) &&
                               !textBoxRecipe.Text.Any(Path.GetInvalidFileNameChars().Contains) &&
                               !_files.Contains(textBoxRecipe.Text);
        }
    }
}
