namespace CloudStorage.NotificationService.Persistence.Repositories;

public interface IFilePublishedInboxRepository
{
    Task<FilePublishedInboxDto> GetFirstUnprocessedWithLeaseAsync(CancellationToken cancellationToken = default);
    
    Task UpdateProcessedAt(Guid inboxId, CancellationToken cancellationToken = default);
}
