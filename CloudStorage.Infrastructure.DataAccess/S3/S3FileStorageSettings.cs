using Amazon.Extensions.NETCore.Setup;

namespace CloudStorage.Infrastructure.DataAccess.S3;

public record S3FileStorageSettings
{
    public required string S3Bucket { get; init; }
    
    public required AWSOptions Aws { get; init; }
}
