using Amazon.S3;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CloudStorage.Infrastructure.DataAccess.S3;

internal sealed class S3FileStorageAsyncInitializer(
    IAmazonS3 amazonS3,
    IOptions<S3FileStorageSettings> options,
    ILogger<S3FileStorageAsyncInitializer> logger) : IAsyncInitializer
{
    public Task InitializeAsync(CancellationToken cancellationToken)
    {
        return amazonS3.EnsureBucketExistsAsync(options.Value.S3Bucket);
    }
}
