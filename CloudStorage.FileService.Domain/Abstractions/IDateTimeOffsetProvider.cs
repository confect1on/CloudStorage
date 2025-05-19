namespace CloudStorage.FileService.Domain.Abstractions;

public interface IDateTimeOffsetProvider
{
    DateTimeOffset GetUtcNow();
}
