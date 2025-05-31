using CloudStorage.NotificationService.Infrastructure.Abstractions.Repositories;

namespace CloudStorage.NotificationService.Infrastructure.Persistence.Repositories;

internal sealed class FilePublishedInboxRepositoryFactory : IFilePublishedInboxRepositoryFactory
{
    public IFilePublishedInboxRepository Create()
    {
        throw new NotImplementedException();
    }
}
