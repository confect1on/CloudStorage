using System.Text.Json;
using Amazon.S3.Model;

namespace CloudStorage.Infrastructure.DataAccess.S3;

internal sealed class FileNotDeletedException(DeleteObjectResponse deleteObjectResponse) : Exception(JsonSerializer.Serialize(deleteObjectResponse));
