using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Cobalt.Avalonia.Desktop.Core.ValueConverters;
using CommunityToolkit.Mvvm.Input;
using MdiAvalonia;

namespace Cobalt.Avalonia.Desktop.Controls.Editors;

public class FileEditor : EditorBase<string?>
{
    public FileEditor()
    {
        ValueConverter ??= ValueConverterFactory.CreateDefaultStringValueConverter();
        var iconsFactory = new IconsFactory();

        ShowFilePickerCommand = new AsyncRelayCommand(ShowFilePicker);

        Buttons!.Add(new Button
        {
            Content = new PathIcon { Data = iconsFactory.CreateGeometry(Icon.file), Width = 16, Height = 16 },
            Command = ShowFilePickerCommand
        });
    }
    
    public static readonly StyledProperty<IFilePickerService?> FilePickerServiceProperty =
        AvaloniaProperty.Register<FileEditor, IFilePickerService?>(nameof(FilePickerService));
    
    public static readonly StyledProperty<string?> StartLocationProperty =
        AvaloniaProperty.Register<EditorBase, string?>(nameof(StartLocation));
    
    public static readonly StyledProperty<IReadOnlyList<FilePickerFileType>?> FiltersProperty =
        AvaloniaProperty.Register<FileEditor, IReadOnlyList<FilePickerFileType>?>(nameof(Filters));

    public IFilePickerService? FilePickerService
    {
        get => GetValue(FilePickerServiceProperty);
        set => SetValue(FilePickerServiceProperty, value);
    }
    
    public string? StartLocation
    {
        get => GetValue(StartLocationProperty);
        set => SetValue(StartLocationProperty, value);
    }

    public IReadOnlyList<FilePickerFileType>? Filters
    {
        get => GetValue(FiltersProperty);
        set => SetValue(FiltersProperty, value);
    }
    
    public AsyncRelayCommand ShowFilePickerCommand { get; }
    
    private async Task ShowFilePicker()
    {
        //TODO: add canExecute to disable button if FilePickerService is not set
        if (FilePickerService is null)
            return;
        
        var res = await FilePickerService.ShowOpenFilePickerAsync("Select file", suggestedStartLocation: StartLocation);
        Value = res.FirstOrDefault();
    }
}