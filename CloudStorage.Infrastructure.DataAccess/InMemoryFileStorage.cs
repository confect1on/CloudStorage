using System.Collections.Concurrent;
using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.Entities.Ids;
using Microsoft.AspNetCore.WebUtilities;

namespace CloudStorage.Infrastructure.DataAccess;

public class InMemoryFileStorage : IFileStorage
{
    private readonly ConcurrentDictionary<StorageId, byte[]> _files = new();
    public async Task<StorageId> UploadFile(Stream stream, CancellationToken cancellationToken = default)
    {
        var buffer = new byte[stream.Length];
        var leastBytes = await stream.ReadAsync(buffer, cancellationToken);
        if (leastBytes > 0)
        {
            throw new InvalidOperationException("File has not fully read");
        }
        var id = new StorageId(Guid.NewGuid().ToString());
        if (!_files.TryAdd(id, buffer))
        {
            throw new InvalidOperationException("File already exists");
        }
        return id;
    }

    public Task<Stream> DownloadFileAsync(StorageId storageId, CancellationToken cancellationToken = default)
    {
        if (!_files.TryGetValue(storageId, out var buffer))
        {
            throw new InvalidOperationException("Storage does not exist");
        }
        return Task.FromResult<Stream>(new MemoryStream(buffer));
    }
}
