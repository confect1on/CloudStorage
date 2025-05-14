using System.Data;
using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.FileManagement.Repositories.FileManagementOutboxRepository;
using CloudStorage.Infrastructure.DataAccess.Persistence.ConnectionFactory;

namespace CloudStorage.Infrastructure.DataAccess.Persistence.Repositories.FileMetadataDeletedOutbox;

internal sealed class FileMetadataDeletedOutboxRepositoryFactory(IConnectionFactory connectionFactory) : IRepositoryFactory<IFileManagementOutboxRepository>
{
    public IFileManagementOutboxRepository Create(IDbConnection dbConnection, IDbTransaction? transaction = null)
    {
        return new FileManagementOutboxRepository(dbConnection, transaction);
    }

    public IFileManagementOutboxRepository Create()
    {
        var connection = connectionFactory.Create();
        return new FileManagementOutboxRepository(connection);
    }
}
