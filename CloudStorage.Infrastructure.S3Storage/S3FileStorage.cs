using Amazon.S3;
using Amazon.S3.Model;
using CloudStorage.FileService.Domain.FileManagement.Repositories;
using CloudStorage.FileService.Domain.FileManagement.ValueObjects;
using Microsoft.Extensions.Options;

namespace CloudStorage.Infrastructure.S3Storage;

internal sealed class S3FileStorage(IAmazonS3 amazonS3, IOptions<S3FileStorageSettings> options) : IFileStorage, ITemporaryFileStorage
{
    public async Task<StorageId> UploadFile(Stream stream, CancellationToken cancellationToken = default)
    {
        var key = await UploadFileInternalAsync(options.Value.S3Bucket, stream, cancellationToken);
        return new StorageId(key);
    }

    public Task<Stream> DownloadFileAsync(TemporaryStorageId temporaryStorageId, CancellationToken cancellationToken = default)
    {
        return DownloadFileInternalAsync(temporaryStorageId, cancellationToken);
    }


    public Task<Stream> DownloadFileAsync(StorageId storageId, CancellationToken cancellationToken = default)
    {
        return DownloadFileInternalAsync(storageId, cancellationToken);
    }


    public async Task DeleteFileAsync(StorageId storageId, CancellationToken cancellationToken = default)
    {
        var result = await amazonS3.DeleteObjectAsync(options.Value.S3Bucket, storageId.ToString(), null, cancellationToken);
        if (result.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new FileNotDeletedException(result);
        }
    }

    async Task<TemporaryStorageId> ITemporaryFileStorage.UploadFile(Stream stream, CancellationToken cancellationToken)
    {
        var key = await UploadFileInternalAsync(options.Value.TemporaryS3Bucket, stream, cancellationToken);
        return new TemporaryStorageId(key);
    }

    private async Task<string> UploadFileInternalAsync(string bucketName, Stream stream, CancellationToken cancellationToken)
    {
        var key = Guid.CreateVersion7().ToString();
        var putObjectRequest = new PutObjectRequest
        {
            BucketName = bucketName,
            InputStream = stream,
            Key = key,
        };
        var putObjectResponse = await amazonS3.PutObjectAsync(putObjectRequest, cancellationToken);
        if (putObjectResponse.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new FileNotUploadedException();
        }
        return key;
    }
    
    
    private Task<Stream> DownloadFileInternalAsync<TId>(TId storageId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(storageId);
        return amazonS3.GetObjectStreamAsync(options.Value.S3Bucket, storageId.ToString(), null, cancellationToken);
    }
}
