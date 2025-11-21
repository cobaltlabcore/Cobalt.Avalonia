using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Cobalt.Avalonia.Desktop.Core.ValueConverters;
using CommunityToolkit.Mvvm.Input;
using MdiAvalonia;

namespace Cobalt.Avalonia.Desktop.Controls.Editors;

public class FolderEditor : EditorBase<string?>
{
    public FolderEditor()
    {
        ValueConverter ??= ValueConverterFactory.CreateDefaultStringValueConverter();
        var iconsFactory = new IconsFactory();

        ShowFolderPickerCommand = new AsyncRelayCommand(ShowFolderPicker);

        Buttons!.Add(new Button
        {
            Content = new PathIcon { Data = iconsFactory.CreateGeometry(Icon.folder_open), Width = 16, Height = 16 },
            Command = ShowFolderPickerCommand
        });
    }
    
    public static readonly StyledProperty<IFolderPickerService?> FolderPickerServiceProperty =
        AvaloniaProperty.Register<FileEditor, IFolderPickerService?>(nameof(FolderPickerService));
    
    public static readonly StyledProperty<string?> StartLocationProperty =
        AvaloniaProperty.Register<EditorBase, string?>(nameof(StartLocation));

    public IFolderPickerService? FolderPickerService
    {
        get => GetValue(FolderPickerServiceProperty);
        set => SetValue(FolderPickerServiceProperty, value);
    }
    
    public string? StartLocation
    {
        get => GetValue(StartLocationProperty);
        set => SetValue(StartLocationProperty, value);
    }
    
    public AsyncRelayCommand ShowFolderPickerCommand { get; }

    private async Task ShowFolderPicker()
    {
        //TODO: add canExecute to disable button if FolderPickerService is not set
        if (FolderPickerService is null)
            return;
        
        var res = await FolderPickerService.ShowOpenFolderPickerAsync("Select folder", suggestedStartLocation: StartLocation);
        Value = res.FirstOrDefault();
    }
}