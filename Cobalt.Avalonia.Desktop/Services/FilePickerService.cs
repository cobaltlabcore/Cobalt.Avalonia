using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace Cobalt.Avalonia.Desktop.Services;

/// <summary>
/// Implementation of <see cref="IFilePickerService"/> that provides file picker functionality.
/// </summary>
public class FilePickerService : IFilePickerService
{
    /// <inheritdoc/>
    public IStorageProvider? StorageProvider { get; private set; }

    /// <inheritdoc/>
    public void SetStorageProvider(IStorageProvider storageProvider)
        => StorageProvider = storageProvider;

    /// <inheritdoc/>
    public async Task<IReadOnlyList<IStorageFile>> ShowOpenFilePickerAsync(FilePickerOpenOptions options)
    {
        if (StorageProvider is null)
            throw new InvalidOperationException("Storage provider is not set");
        return await StorageProvider.OpenFilePickerAsync(options);
    }

    /// <inheritdoc/>
    public async Task<IStorageFile?> ShowSaveFilePickerAsync(FilePickerSaveOptions options)
    {
        if (StorageProvider is null)
            throw new InvalidOperationException("Storage provider is not set");
        return await StorageProvider.SaveFilePickerAsync(options);
    }
}