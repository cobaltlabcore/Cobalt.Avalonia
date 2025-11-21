using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Cobalt.Avalonia.Desktop;
using TesterApp.Pages.ViewModels;

namespace TesterApp.Pages;

public partial class EditorsPage : UserControl, INavigationPage
{
    public EditorsPage(EditorsPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    
    public void OnNavigatingTo()
    {
        
    }

    public void OnNavigatedFrom()
    {
        
    }
}