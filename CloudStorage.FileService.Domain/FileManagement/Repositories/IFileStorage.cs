﻿using CloudStorage.FileService.Domain.FileManagement.ValueObjects;

namespace CloudStorage.FileService.Domain.FileManagement.Repositories;

public interface IFileStorage
{
    Task<StorageId> UploadFile(Stream stream, CancellationToken cancellationToken = default);
    
    Task<Stream> DownloadFileAsync(StorageId storageId, CancellationToken cancellationToken = default);
    
    Task DeleteFileAsync(StorageId storageId, CancellationToken cancellationToken = default);
}
