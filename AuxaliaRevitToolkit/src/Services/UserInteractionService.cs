using System.Windows;

namespace AuxaliaRevitToolkit.Services
{
    public class UserInteractionService : IUserInteractionService
    {
        public bool ConfirmDeletion(string itemName)
        {
            var result = MessageBox.Show($"Are you sure you want to delete the level '{itemName}'?",
                                         "Confirm Deletion",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Warning);
            return result == MessageBoxResult.Yes;
        }

        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
