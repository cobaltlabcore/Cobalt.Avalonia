using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Cobalt.Avalonia.Desktop;
using Cobalt.Avalonia.Desktop.Core;
using Microsoft.Extensions.DependencyInjection;
using TesterApp.Windows;
using TesterApp.Windows.ViewModels;

namespace TesterApp;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        var serviceProvider = Program.Bootstrapper.ServiceProvider;
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var splashScreenViewModel = serviceProvider.GetRequiredService<SplashScreenViewModel>();
            var splashScreen = serviceProvider.GetRequiredService<SplashScreenWindow>();
            splashScreen.DataContext = splashScreenViewModel;
            desktop.MainWindow = splashScreen;
            splashScreen.Show();

            await DoSomethingAsync(splashScreenViewModel);
            
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Closed += MainWindowOnClosed;

            serviceProvider.GetRequiredService<IFilePickerService>().SetStorageProvider(mainWindow.StorageProvider);
            serviceProvider.GetRequiredService<IFolderPickerService>().SetStorageProvider(mainWindow.StorageProvider);
            serviceProvider.GetRequiredService<IOverlayService>().SetOverlayHost(mainWindow.OverlayHost);
            
            // var navigationService = serviceProvider.GetRequiredService<INavigationService>();
            // navigationService.Initialize(mainWindow.NavigationView, mainWindow.NavigationViewFrame);
            
            
            desktop.MainWindow = mainWindow;
            mainWindow.Show();
            splashScreen.Close();
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    private async Task DoSomethingAsync(IProgress progress)
    {
        progress.UpdateProgress(
            isIndeterminate: true,
            message: "Doing something...");
        
        await Task.Delay(1500);

        progress.UpdateProgress(
            isIndeterminate: false,
            message: "Doing something else...");
        const int total = 200;
        var current = 0;
        while (current++ < total)
        {
            progress.UpdateProgress(value: current / (double) total * 100);
            await Task.Delay(10);
            // current++;
        }
    }

    private async void MainWindowOnClosed(object? sender, EventArgs e)
    {
        await Program.Bootstrapper.StopAsync();
        (ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.Shutdown();
    }
}