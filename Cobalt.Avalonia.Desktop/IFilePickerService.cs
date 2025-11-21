using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace Cobalt.Avalonia.Desktop;

/// <summary>
/// Provides an abstraction for file picker operations.
/// </summary>
public interface IFilePickerService
{
    /// <summary>
    /// Gets the storage provider used for file operations.
    /// </summary>
    IStorageProvider? StorageProvider { get; }

    /// <summary>
    /// Sets the storage provider to be used for file operations.
    /// </summary>
    /// <param name="storageProvider">The storage provider to set.</param>
    void SetStorageProvider(IStorageProvider storageProvider);

    /// <summary>
    /// Shows an open file picker dialog with the specified options.
    /// </summary>
    /// <param name="options">The options for the file picker dialog.</param>
    /// <returns>A list of selected storage files.</returns>
    Task<IReadOnlyList<IStorageFile>> ShowOpenFilePickerAsync(FilePickerOpenOptions options);

    /// <summary>
    /// Shows a save file picker dialog with the specified options.
    /// </summary>
    /// <param name="options">The options for the save file picker dialog.</param>
    /// <returns>The selected storage file or null if canceled.</returns>
    Task<IStorageFile?> ShowSaveFilePickerAsync(FilePickerSaveOptions options);
}