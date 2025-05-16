using Amazon.Extensions.NETCore.Setup;

namespace CloudStorage.Infrastructure.S3Storage;

public record S3FileStorageSettings
{
    public required string S3Bucket { get; init; }
    
    public required string TemporaryS3Bucket { get; init; }
    
    public required AWSOptions Aws { get; init; }
}
