using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using FocalSpec.GuiExample.Model;
using FocalSpec.GuiExample.Model.Camera;

namespace FocalSpec.GuiExample.View
{
    public partial class RecipeSelectView : Form
    {
        public int SensorType;
        public int SelectedSensorType;
        public string SelectedRecipe;

        private readonly Dictionary<string, string> _recipes = new Dictionary<string, string>();


        public RecipeSelectView()
        {
            InitializeComponent();
        }

        private void LoadRecipeView_Load(object sender, System.EventArgs e)
        {
            if (!comboBoxSensorTypes.Items.Contains(SensorType.ToString()))
                comboBoxSensorTypes.Items.Add(SensorType.ToString());
            _recipes.Clear();
            if (Directory.Exists(Defines.RecipeFolder))
            {
                var files = Directory.GetFiles(Defines.RecipeFolder);
                foreach (var file in files)
                {
                    foreach (var line in File.ReadAllLines(file))
                    {
                        if (line.Contains("\"sensor_type\":"))
                        {
                            char[] charsToTrim = {' ', ','};
                            var type = line.Trim();
                            type = type.Length > 15 ? type.Substring(15).Trim(charsToTrim) : "";
                            if (int.TryParse(type, out _))
                                _recipes.Add(Path.GetFileNameWithoutExtension(file), type);
                        }
                    }
                }

                foreach (var recipe in _recipes)
                {
                    if (!comboBoxSensorTypes.Items.Contains(recipe.Value))
                        comboBoxSensorTypes.Items.Add(recipe.Value);
                }
            }

            int selectedTypeIndex = comboBoxSensorTypes.FindStringExact(SensorType.ToString());
            comboBoxSensorTypes.SelectedIndex = selectedTypeIndex;
            UpdateRecipes();

            if (listBoxRecipes.Items.Count > 0)
                listBoxRecipes.SelectedItem = SelectedRecipe;
        }

        private void LoadRecipeView_Shown(object sender, System.EventArgs e)
        {

        }

        private void LoadRecipeView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.Cancel) return;
            var type = int.Parse(comboBoxSensorTypes.SelectedItem.ToString());
            if (type != SensorType)
            {
                var message = $"Selected recipe is not intended for the {SensorType} sensor. \nDo you want to continue?";
                if (MessageBox.Show(message, @"Select Recipe", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            SelectedSensorType = type;
            SelectedRecipe = listBoxRecipes.SelectedItem == null ? "" : listBoxRecipes.SelectedItem.ToString();
            ReadSpecialParameters();
        }

        private void comboBoxSensorTypes_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateRecipes();
        }

        private void UpdateRecipes()
        {
            listBoxRecipes.Items.Clear();
            var type = comboBoxSensorTypes.SelectedItem.ToString();
            foreach (var recipe in _recipes)
            {
                if (recipe.Value == type && !listBoxRecipes.Items.Contains(recipe.Key))
                {
                    listBoxRecipes.Items.Add(recipe.Key);
                }
            }
        }

        private void ReadSpecialParameters()
        {
            var file = $"{Defines.RecipeFolder}{SelectedRecipe}.json";
            if (!File.Exists(file)) return;

            foreach (var line in File.ReadAllLines(file))
            {
                if (line.Contains("\"image_offsety\":"))
                {
                    char[] charsToTrim = { ' ', ',' };
                    var offset = line.Trim();
                    offset = offset.Length > 17 ? offset.Substring(17).Trim(charsToTrim) : "";
                    if (int.TryParse(offset, out int offsetY))
                        SensorParameterStore.GetInstance().OffsetY = offsetY;
                }
            }
        }
    }
}
