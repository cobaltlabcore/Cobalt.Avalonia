using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace Cobalt.Avalonia.Desktop.Services;

/// <summary>
/// Implementation of <see cref="IFolderPickerService"/> that provides folder picker functionality.
/// </summary>
public class FolderPickerService : IFolderPickerService
{
    /// <inheritdoc/>
    public IStorageProvider? StorageProvider { get; private set; }

    /// <inheritdoc/>
    public void SetStorageProvider(IStorageProvider storageProvider)
        => StorageProvider = storageProvider;

    /// <inheritdoc/>
    public async Task<IReadOnlyList<IStorageFolder>> ShowOpenFolderPickerAsync(FolderPickerOpenOptions options)
    {
        if (StorageProvider is null)
            throw new InvalidOperationException("Storage provider is not set");
        return await StorageProvider.OpenFolderPickerAsync(options);
    }
}