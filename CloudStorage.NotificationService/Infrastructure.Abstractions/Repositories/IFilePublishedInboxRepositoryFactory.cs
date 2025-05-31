namespace CloudStorage.NotificationService.Infrastructure.Abstractions.Repositories;

public interface IFilePublishedInboxRepositoryFactory
{
    IFilePublishedInboxRepository Create();
}
