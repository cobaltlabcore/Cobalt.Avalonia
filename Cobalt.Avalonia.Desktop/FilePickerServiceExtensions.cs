using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace Cobalt.Avalonia.Desktop;

/// <summary>
/// Extension methods for <see cref="IFilePickerService"/> that provide simplified overloads with string-based paths.
/// </summary>
public static class FilePickerServiceExtensions
{
    /// <summary>
    /// Shows an open file picker dialog with simplified parameters and returns file paths as strings.
    /// </summary>
    /// <param name="filePickerService">The file picker service.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="allowMultiple">Whether to allow multiple file selection.</param>
    /// <param name="suggestedStartLocation">The suggested starting location path.</param>
    /// <param name="suggestedFileName">The suggested file name.</param>
    /// <param name="fileTypeFilter">The file type filter.</param>
    /// <returns>An enumerable of selected file paths as strings.</returns>
    /// <exception cref="InvalidOperationException">Thrown when storage provider is not set.</exception>
    public static async Task<IEnumerable<string>> ShowOpenFilePickerAsync(
        this IFilePickerService filePickerService,
        string? title = null,
        bool allowMultiple = false,
        string? suggestedStartLocation = null,
        string? suggestedFileName = null,
        IReadOnlyList<FilePickerFileType>? fileTypeFilter = null)
    {
        if (filePickerService.StorageProvider is null)
            throw new InvalidOperationException("Storage provider is not set");

        var options = new FilePickerOpenOptions();

        if (title is not null)
            options.Title = title;
        options.AllowMultiple = allowMultiple;
        if (suggestedStartLocation is not null)
            options.SuggestedStartLocation =
                await filePickerService.StorageProvider.TryGetFolderFromPathAsync(suggestedStartLocation);
        if (suggestedFileName is not null)
            options.SuggestedFileName = suggestedFileName;
        options.FileTypeFilter = fileTypeFilter;

        var files = await filePickerService.ShowOpenFilePickerAsync(options);

        List<string> paths = [];
        paths.AddRange(files.Select(folder => folder.TryGetLocalPath()).OfType<string>());

        return paths;
    }

    /// <summary>
    /// Shows a save file picker dialog with simplified parameters and returns the file path as a string.
    /// </summary>
    /// <param name="filePickerService">The file picker service.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="suggestedStartLocation">The suggested starting location path.</param>
    /// <param name="suggestedFileName">The suggested file name.</param>
    /// <param name="defaultExtension">The default file extension.</param>
    /// <param name="showOverwritePrompt">Whether to show an overwrite prompt.</param>
    /// <param name="fileTypeChoices">The available file type choices.</param>
    /// <returns>The selected file path as a string, or null if canceled.</returns>
    /// <exception cref="InvalidOperationException">Thrown when storage provider is not set.</exception>
    public static async Task<string?> ShowSaveFilePickerAsync(
        this IFilePickerService filePickerService,
        string? title = null,
        string? suggestedStartLocation = null,
        string? suggestedFileName = null,
        string? defaultExtension = null,
        bool showOverwritePrompt = true,
        IReadOnlyList<FilePickerFileType>? fileTypeChoices = null)
    {
        if (filePickerService.StorageProvider is null)
            throw new InvalidOperationException("Storage provider is not set");
        
        var options = new FilePickerSaveOptions();
        
        if (title is not null)
            options.Title = title;
        if (suggestedStartLocation is not null)
            options.SuggestedStartLocation =
                await filePickerService.StorageProvider.TryGetFolderFromPathAsync(suggestedStartLocation);
        if (suggestedFileName is not null)
            options.SuggestedFileName = suggestedFileName;
        if (defaultExtension is not null)
            options.DefaultExtension = defaultExtension;
        options.ShowOverwritePrompt = showOverwritePrompt;
        if (fileTypeChoices is not null)
            options.FileTypeChoices = fileTypeChoices;
        
        var file = await filePickerService.ShowSaveFilePickerAsync(options);
        return file?.TryGetLocalPath();
    }
}