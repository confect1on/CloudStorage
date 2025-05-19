using CloudStorage.FileService.Domain.Abstractions;

namespace CloudStorage.FileService.Domain.DomainServices;

internal sealed class DateTimeOffsetProvider : IDateTimeOffsetProvider
{
    public DateTimeOffset GetUtcNow() => DateTimeOffset.UtcNow;
}
