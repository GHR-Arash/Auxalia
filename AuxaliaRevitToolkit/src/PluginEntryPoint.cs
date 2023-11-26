using Autodesk.Revit.UI;
using AuxaliaRevitToolkit.Services;
using AuxaliaRevitToolkit.Utilities;
using AuxaliaRevitToolkit.ViewModels;
using AuxaliaRevitToolkit.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

public class RevitApp : IExternalApplication
{
    internal static ServiceProvider ServiceProvider { get; private set; }
    internal static IServiceProvider GlobalServiceProvider { get; private set; }

    public Result OnStartup(UIControlledApplication application)
    {
        // Launch the debugger
        System.Diagnostics.Debugger.Launch();

        var services = new ServiceCollection();
        ConfigureServices(services);
        GlobalServiceProvider = services.BuildServiceProvider();
        return Result.Succeeded;
    }

    public Result OnShutdown(UIControlledApplication application)
    {
        ServiceProvider?.Dispose();
        return Result.Succeeded;
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IUserInteractionService, UserInteractionService>();
        services.AddSingleton<ILevelCreationDialogService, LevelCreationDialogService>();


        services.AddSingleton<UIApplicationWrapper>();
        services.AddSingleton<RevitService>();

        services.AddSingleton<LevelManagementViewModel>();
        services.AddTransient<LevelManagementView>();


    }
}
