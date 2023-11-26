using System.Windows;

namespace AuxaliaRevitToolkit.Views
{
    public partial class NewLevelDialog : Window
    {
        public string LevelName { get; private set; }
        public double LevelElevation { get; private set; }

        public NewLevelDialog()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate name
            var name = NameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Name cannot be empty.");
                return;
            }

            // Validate elevation
            if (!double.TryParse(ElevationTextBox.Text, out double elevation))
            {
                MessageBox.Show("Invalid elevation value.");
                return;
            }

            LevelName = name;
            LevelElevation = elevation;
            this.DialogResult = true;
        }
    }
}
