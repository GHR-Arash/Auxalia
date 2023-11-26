using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using AuxaliaRevitToolkit.Utilities;
using AuxaliaRevitToolkit.ViewModels;
using AuxaliaRevitToolkit.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace AuxaliaRevitToolkit.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class LevelManagementCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            var uiAppWrapper = RevitApp.GlobalServiceProvider.GetService<UIApplicationWrapper>();
            uiAppWrapper.UIApplication = commandData.Application;

            var view = RevitApp.GlobalServiceProvider.GetService<LevelManagementView>();
            view.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Show the view or perform other actions as required
            view.ShowDialog();

            return Result.Succeeded;
        }
    }
}
