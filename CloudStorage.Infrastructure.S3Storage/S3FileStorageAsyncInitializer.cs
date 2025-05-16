using Amazon.S3;
using Amazon.S3.Model;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.Extensions.Options;

namespace CloudStorage.Infrastructure.S3Storage;

internal sealed class S3FileStorageAsyncInitializer(
    IAmazonS3 amazonS3,
    IOptions<S3FileStorageSettings> options) : IAsyncInitializer
{
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        await CreateBucketIfNotExists(options.Value.S3Bucket);
        await CreateBucketIfNotExists(options.Value.TemporaryS3Bucket);
    }

    private async Task CreateBucketIfNotExists(string bucketName)
    {
        try
        {
            await amazonS3.EnsureBucketExistsAsync(bucketName);
        }
        catch (BucketAlreadyOwnedByYouException)
        {
            // BUG: https://github.com/aws/aws-sdk-net/issues/3807
        }
    }
}
