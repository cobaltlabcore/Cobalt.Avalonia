using Cobalt.Avalonia.Desktop;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.UI.Controls;
using MdiAvalonia;
using TesterApp.Pages;

namespace TesterApp.Windows.ViewModels;

public class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        InitializeNavigationItems();
    }
    
    public INavigationService NavigationService { get; }
    
    private void InitializeNavigationItems()
    {
        var iconsFactory = new IconsFactory();
        
        NavigationService.MenuItems.Add(new NavigationViewItem
        {
            Content = "Home",
            Tag = typeof(HomePage),
            IconSource = new PathIconSource 
            { 
                Data = iconsFactory.CreateGeometry(Icon.home)
            }
        });
        NavigationService.MenuItems.Add(new NavigationViewItem
        {
            Content = "Editors",
            Tag = typeof(EditorsPage),
            IconSource = new PathIconSource 
            { 
                Data = iconsFactory.CreateGeometry(Icon.form_textbox)
            }
        });
        NavigationService.MenuItems.Add(new NavigationViewItem
        {
            Content = "File pickers",
            Tag = typeof(FilePickerPage),
            IconSource = new PathIconSource 
            { 
                Data = iconsFactory.CreateGeometry(Icon.file)
            }
        });
        
        NavigationService.FooterMenuItems.Add(new NavigationViewItem
        {
            Content = "Settings",
            Tag = typeof(SettingsPage),
            IconSource = new PathIconSource 
            { 
                Data = iconsFactory.CreateGeometry(Icon.cog)
            }
        });
    }
}