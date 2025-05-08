using CloudStorage.Domain.Entities.Ids;

namespace CloudStorage.Domain.Abstractions;

public interface IFileStorage
{
    Task<StorageId> UploadFile(Stream fileStream, CancellationToken cancellationToken = default);
    
    Task<Stream> DownloadFileAsync(StorageId storageId, CancellationToken cancellationToken = default);
}
