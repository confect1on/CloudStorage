using CloudStorage.FileService.Domain.Abstractions;
using CloudStorage.FileService.Domain.FileManagement;
using CloudStorage.FileService.Domain.FileManagement.Repositories.FileManagementOutboxRepository;
using CloudStorage.Infrastructure.Persistence.ConnectionFactory;

namespace CloudStorage.Infrastructure.Persistence;

internal sealed class DapperUnitOfWorkFactory(
    IRepositoryFactory<IFileMetadataRepository> fileMetadataRepositoryFactory, 
    IRepositoryFactory<IFileManagementOutboxRepository> fileMetadataDeletedOutboxRepositoryFactory, 
    IConnectionFactory connectionFactory) : IUnitOfWorkFactory
{
    public IUnitOfWork Create()
    {
        return new DapperUnitOfWork(
            fileMetadataRepositoryFactory,
            fileMetadataDeletedOutboxRepositoryFactory,
            connectionFactory);
    }
}
