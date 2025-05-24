using CloudStorage.NotificationService.Persistence.Repositories;

namespace CloudStorage.NotificationService.Infrastructure.Persistence.Repositories;

internal sealed class FilePublishedInboxRepositoryFactory : IFilePublishedInboxRepositoryFactory
{
    public IFilePublishedInboxRepository Create()
    {
        throw new NotImplementedException();
    }
}
