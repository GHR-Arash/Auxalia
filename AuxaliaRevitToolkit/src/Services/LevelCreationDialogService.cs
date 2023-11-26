using AuxaliaRevitToolkit.Views;
using System.Windows;

namespace AuxaliaRevitToolkit.Services
{
    public class LevelCreationDialogService : ILevelCreationDialogService
    {
        public bool ShowDialog(out string levelName, out double levelElevation)
        {
            levelName = string.Empty;
            levelElevation = 0.0;

            var dialog = new NewLevelDialog();
            var result = dialog.ShowDialog();

            if (result == true)
            {
                levelName = dialog.LevelName;
                levelElevation = dialog.LevelElevation;
                return true;
            }
            return false;
        }
    }
}
