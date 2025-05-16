using CloudStorage.Domain.FileManagement.ValueObjects;

namespace CloudStorage.Domain.FileManagement.Repositories;

public interface ITemporaryFileStorage
{
    Task<TemporaryStorageId> UploadFile(Stream stream, CancellationToken cancellationToken = default);
    
    Task<Stream> DownloadFileAsync(TemporaryStorageId temporaryStorageId, CancellationToken cancellationToken = default);
}
