using Avalonia;
using System;
using System.Threading.Tasks;
using Cobalt.Avalonia.Desktop;
using Cobalt.Avalonia.Desktop.Bootstrapper;
using Cobalt.Avalonia.Desktop.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TesterApp.Pages;
using TesterApp.Pages.ViewModels;
using TesterApp.Windows;
using TesterApp.Windows.ViewModels;

namespace TesterApp;

class Program
{
    private static AppBootstrapper? _bootstrapper;
    public static AppBootstrapper Bootstrapper => _bootstrapper
                                                  ?? throw new InvalidOperationException("Bootstrapper is not set");
    
    [STAThread]
    public static async Task Main(string[] args)
    {
        try
        {
            _bootstrapper = new AppBootstrapper(args);
            await Bootstrapper.StartAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}

public class AppBootstrapper(string[] args) : AvaloniaBootstrapper(args)
{
    protected override Func<Application> AppFactory => () => new App();

    protected override void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        base.ConfigureServices(context, services);

        // _ = services.AddSingleton<INavigationService, NavigationService>();
        _ = services.AddSingleton<IFilePickerService, FilePickerService>();
        _ = services.AddSingleton<IFolderPickerService, FolderPickerService>();
        _ = services.AddSingleton<IOverlayService, OverlayService>();
        _ = services.AddSingleton<IContentDialogService, ContentDialogService>();
        _ = services.AddSingleton<MainWindow>();
        _ = services.AddSingleton<MainWindowViewModel>();
        _ = services.AddSingleton<SplashScreenWindow>();
        _ = services.AddSingleton<SplashScreenViewModel>();
        _ = services.AddSingleton<HomePage>();
        _ = services.AddSingleton<HomePageViewModel>();
        _ = services.AddSingleton<EditorsPage>();
        _ = services.AddSingleton<EditorsPageViewModel>();
        _ = services.AddSingleton<FilePickerPage>();
        _ = services.AddSingleton<FilePickerPageViewModel>();
        _ = services.AddSingleton<SettingsPage>();
        _ = services.AddSingleton<SettingsPageViewModel>();
        _ = services.AddSingleton<TestPage>();
        _ = services.AddSingleton<TestPageViewModel>();
    }
}