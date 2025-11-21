using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TesterApp.Pages.ViewModels;

public class SettingsPageViewModel : ObservableObject
{
    public string[] AppThemes => [ "Light", "Dark" ];
    
    public string SelectedAppTheme
    {
        get => _selectedAppTheme;
        set
        {
            if (SetProperty(ref _selectedAppTheme, value))
            {
                switch (value)
                {
                    case "Light":
                        Application.Current!.RequestedThemeVariant = ThemeVariant.Light;
                        break;
                    case "Dark":
                        Application.Current!.RequestedThemeVariant = ThemeVariant.Dark;
                        break;
                }
            }
        }
    }
    private string _selectedAppTheme = "Dark";
}