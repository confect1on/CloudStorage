using CloudStorage.Domain.Abstractions;

namespace CloudStorage.Domain.DomainServices;

internal sealed class DateTimeOffsetProvider : IDateTimeOffsetProvider
{
    public DateTimeOffset GetUtcNow() => DateTimeOffset.UtcNow;
}
