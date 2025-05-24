using CloudStorage.NotificationService.Persistence.Repositories;

namespace CloudStorage.NotificationService.Infrastructure.Persistence.Repositories;

public interface IFilePublishedInboxRepositoryFactory
{
    IFilePublishedInboxRepository Create();
}
