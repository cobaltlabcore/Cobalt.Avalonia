using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cobalt.Avalonia.Desktop;
using TesterApp.Pages.ViewModels;

namespace TesterApp.Pages;

public partial class SettingsPage : UserControl, INavigationPage
{
    public SettingsPage(SettingsPageViewModel pageViewModel)
    {
        InitializeComponent();
        DataContext = pageViewModel;
    }

    public void OnNavigatingTo()
    {
        
    }

    public void OnNavigatedFrom()
    {
        
    }
}