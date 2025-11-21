using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using Cobalt.Avalonia.Desktop;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TesterApp.Pages.ViewModels;

public class FilePickerPageViewModel : ObservableObject
{
    private readonly IFilePickerService _filePickerService;
    private readonly IFolderPickerService _folderPickerService;

    public FilePickerPageViewModel(
        IFilePickerService filePickerService,
        IFolderPickerService folderPickerService)
    {
        _filePickerService = filePickerService;
        _folderPickerService = folderPickerService;
        ShowOpenFilePickerCommand = new AsyncRelayCommand(ShowOpenFilePicker);
        ShowSaveFilePickerCommand = new AsyncRelayCommand(ShowSaveFilePicker);
        ShowOpenFolderPickerCommand = new AsyncRelayCommand(ShowOpenFolderPicker); 
    }
    
    public AsyncRelayCommand ShowOpenFilePickerCommand { get; }
    public AsyncRelayCommand ShowSaveFilePickerCommand { get; }
    public AsyncRelayCommand ShowOpenFolderPickerCommand { get; }
    
    private async Task ShowOpenFilePicker()
    {
        var res = await _filePickerService.ShowOpenFilePickerAsync(
            title: "Open file",
            allowMultiple: true,
            fileTypeFilter:
            [
                new FilePickerFileType("All files") { Patterns = ["*.*"]},
                new FilePickerFileType("Image files") { Patterns = ["*.png", "*.jpg", "*.jpeg"]},
                new FilePickerFileType("project files") { Patterns = ["*.sln", "*.csproj"]}
            ]);
    }
    
    private async Task ShowSaveFilePicker()
    {
        var res = await _filePickerService.ShowSaveFilePickerAsync(
            title: "Save file",
            suggestedFileName: "test.txt",
            defaultExtension: "txt",
            showOverwritePrompt: true);
    }
    
    private async Task ShowOpenFolderPicker()
    {
        var res = await _folderPickerService.ShowOpenFolderPickerAsync(
            title: "Open folder",
            allowMultiple: true);
    } 
}