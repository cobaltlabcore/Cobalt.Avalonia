using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cobalt.Avalonia.Desktop;
using TesterApp.Pages.ViewModels;

namespace TesterApp.Pages;

public partial class HomePage : UserControl, INavigationPage
{
    public HomePage(HomePageViewModel pageViewModel)
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