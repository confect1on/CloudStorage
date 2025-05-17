namespace AVScannerService.Persistence.Abstractions;

public interface IFileCreatedEventInboxRepository
{
    Task InsertAsync(FileCreatedEventDto fileCreatedEventDto, CancellationToken cancellationToken);
}
