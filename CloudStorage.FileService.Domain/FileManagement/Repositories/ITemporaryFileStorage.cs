using CloudStorage.FileService.Domain.FileManagement.ValueObjects;

namespace CloudStorage.FileService.Domain.FileManagement.Repositories;

public interface ITemporaryFileStorage
{
    Task<TemporaryStorageId> UploadFile(Stream stream, CancellationToken cancellationToken = default);
    
    Task<Stream> DownloadFileAsync(TemporaryStorageId temporaryStorageId, CancellationToken cancellationToken = default);
}
