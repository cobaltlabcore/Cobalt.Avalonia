using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace Cobalt.Avalonia.Desktop;

/// <summary>
/// Extension methods for <see cref="IFolderPickerService"/> that provide simplified overloads with string-based paths.
/// </summary>
public static class FolderPickerServiceExtensions
{
    /// <summary>
    /// Shows an open folder picker dialog with simplified parameters and returns folder paths as strings.
    /// </summary>
    /// <param name="folderPickerService">The folder picker service.</param>
    /// <param name="title">The title of the dialog.</param>
    /// <param name="allowMultiple">Whether to allow multiple folder selection.</param>
    /// <param name="suggestedStartLocation">The suggested starting location path.</param>
    /// <param name="suggestedFileName">The suggested file name.</param>
    /// <returns>An enumerable of selected folder paths as strings.</returns>
    /// <exception cref="InvalidOperationException">Thrown when storage provider is not set.</exception>
    public static async Task<IEnumerable<string>> ShowOpenFolderPickerAsync(
        this IFolderPickerService folderPickerService,
        string? title = null,
        bool allowMultiple = false,
        string? suggestedStartLocation = null,
        string? suggestedFileName = null)
    {
        if (folderPickerService.StorageProvider is null)
            throw new InvalidOperationException("Storage provider is not set");
        
        var options = new FolderPickerOpenOptions();
        
        if (title is not null)
            options.Title = title;
        options.AllowMultiple = allowMultiple;
        if (suggestedStartLocation is not null)
            options.SuggestedStartLocation =
                await folderPickerService.StorageProvider.TryGetFolderFromPathAsync(suggestedStartLocation);
        if (suggestedFileName is not null)
            options.SuggestedFileName = suggestedFileName;
        
        var folders = await folderPickerService.ShowOpenFolderPickerAsync(options);
        
        List<string> paths = [];
        paths.AddRange(folders.Select(folder => folder.TryGetLocalPath()).OfType<string>());

        return paths;
    }
}