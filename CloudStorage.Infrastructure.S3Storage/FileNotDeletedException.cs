using System.Text.Json;
using Amazon.S3.Model;

namespace CloudStorage.Infrastructure.S3Storage;

internal sealed class FileNotDeletedException(DeleteObjectResponse deleteObjectResponse) : Exception(JsonSerializer.Serialize(deleteObjectResponse));
