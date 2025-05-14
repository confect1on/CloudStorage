using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.FileManagement;
using CloudStorage.Domain.FileManagement.Repositories.FileManagementOutboxRepository;
using CloudStorage.Infrastructure.DataAccess.Persistence.ConnectionFactory;

namespace CloudStorage.Infrastructure.DataAccess.Persistence;

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
