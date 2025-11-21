using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace Cobalt.Avalonia.Desktop;

/// <summary>
/// Provides an abstraction for folder picker operations.
/// </summary>
public interface IFolderPickerService
{
    /// <summary>
    /// Gets the storage provider used for folder operations.
    /// </summary>
    IStorageProvider? StorageProvider { get; }

    /// <summary>
    /// Sets the storage provider to be used for folder operations.
    /// </summary>
    /// <param name="storageProvider">The storage provider to set.</param>
    void SetStorageProvider(IStorageProvider storageProvider);

    /// <summary>
    /// Shows an open folder picker dialog with the specified options.
    /// </summary>
    /// <param name="options">The options for the folder picker dialog.</param>
    /// <returns>A list of selected storage folders.</returns>
    Task<IReadOnlyList<IStorageFolder>> ShowOpenFolderPickerAsync(FolderPickerOpenOptions options);
}