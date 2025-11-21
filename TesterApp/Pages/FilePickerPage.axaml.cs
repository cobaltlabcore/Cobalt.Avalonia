using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cobalt.Avalonia.Desktop;
using TesterApp.Pages.ViewModels;

namespace TesterApp.Pages;

public partial class FilePickerPage : UserControl, INavigationPage
{
    public FilePickerPage(FilePickerPageViewModel pageViewModel)
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