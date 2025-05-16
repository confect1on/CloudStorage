using Amazon.S3;
using Amazon.S3.Model;
using CloudStorage.Domain.FileManagement.Repositories;
using CloudStorage.Domain.FileManagement.ValueObjects;
using Microsoft.Extensions.Options;

namespace CloudStorage.Infrastructure.S3Storage;

internal sealed class S3FileStorage(IAmazonS3 amazonS3, IOptions<S3FileStorageSettings> options) : IFileStorage
{
    public async Task<StorageId> UploadFile(Stream stream, CancellationToken cancellationToken = default)
    {
        var key = Guid.CreateVersion7().ToString();
        var putObjectRequest = new PutObjectRequest
        {
            BucketName = options.Value.S3Bucket,
            InputStream = stream,
            Key = key,
        };
        var putObjectResponse = await amazonS3.PutObjectAsync(putObjectRequest, cancellationToken);
        if (putObjectResponse.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new FileNotUploadedException();
        }
        return new StorageId(key);
    }

    public Task<Stream> DownloadFileAsync(StorageId storageId, CancellationToken cancellationToken = default)
    {
        return amazonS3.GetObjectStreamAsync(options.Value.S3Bucket, storageId.ToString(), null, cancellationToken);
    }

    public async Task DeleteFileAsync(StorageId storageId, CancellationToken cancellationToken = default)
    {
        var result = await amazonS3.DeleteObjectAsync(options.Value.S3Bucket, storageId.ToString(), null, cancellationToken);
        if (result.HttpStatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new FileNotDeletedException(result);
        }
    }
}
