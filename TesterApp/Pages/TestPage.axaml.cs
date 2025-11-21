using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cobalt.Avalonia.Desktop;
using TesterApp.Pages.ViewModels;

namespace TesterApp.Pages;

public partial class TestPage : UserControl, INavigationPage
{
    public TestPage(TestPageViewModel pageViewModel)
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