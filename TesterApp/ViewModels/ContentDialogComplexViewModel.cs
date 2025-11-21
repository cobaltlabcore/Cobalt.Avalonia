using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.UI.Controls;

namespace TesterApp.ViewModels;

public class ContentDialogComplexViewModel : ObservableObject
{
    private readonly ContentDialog _dialog;

    public ContentDialogComplexViewModel(ContentDialog dialog)
    {
        _dialog = dialog;
        _dialog.IsPrimaryButtonEnabled = false;
    }
    
    public string? Pass1
    {
        get => _pass1;
        set
        {
            if (SetProperty(ref _pass1, value))
                Validate();
        }
    }
    private string? _pass1;
    
    public string? Pass2
    {
        get => _pass2;
        set
        {
            if (SetProperty(ref _pass2, value))
                Validate();
        }
    }
    private string? _pass2;

    private void Validate()
    {
        _dialog.IsPrimaryButtonEnabled = Pass1 is not null && Pass2 is not null && Pass1.Length > 0 && Pass1 == Pass2;
    }
}