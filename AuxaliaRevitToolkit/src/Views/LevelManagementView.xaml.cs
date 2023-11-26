using AuxaliaRevitToolkit.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AuxaliaRevitToolkit.Views
{
    public partial class LevelManagementView : Window
    {

        public LevelManagementView(LevelManagementViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}

