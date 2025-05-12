using Amazon.S3;
using Amazon.S3.Model;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.Extensions.Options;

namespace CloudStorage.Infrastructure.DataAccess.S3;

internal sealed class S3FileStorageAsyncInitializer(
    IAmazonS3 amazonS3,
    IOptions<S3FileStorageSettings> options) : IAsyncInitializer
{
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        try
        {
            await amazonS3.EnsureBucketExistsAsync(options.Value.S3Bucket);
        }
        catch (BucketAlreadyOwnedByYouException)
        {
            // BUG: https://github.com/aws/aws-sdk-net/issues/3807
        }
    }
}
