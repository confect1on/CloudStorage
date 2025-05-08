namespace CloudStorage.Domain.Abstractions;

public interface IDateTimeOffsetProvider
{
    DateTimeOffset GetUtcNow();
}
