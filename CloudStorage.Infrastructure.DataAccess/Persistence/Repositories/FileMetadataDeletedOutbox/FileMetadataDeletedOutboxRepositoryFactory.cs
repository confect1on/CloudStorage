using System.Data;
using CloudStorage.Domain.Abstractions;
using CloudStorage.Infrastructure.DataAccess.Persistence.ConnectionFactory;

namespace CloudStorage.Infrastructure.DataAccess.Persistence.Repositories.FileMetadataDeletedOutbox;

internal sealed class FileMetadataDeletedOutboxRepositoryFactory(IConnectionFactory connectionFactory) : IRepositoryFactory<IFileMetadataDeletedOutboxRepository>
{
    public IFileMetadataDeletedOutboxRepository Create(IDbConnection dbConnection, IDbTransaction? transaction = null)
    {
        return new FileMetadataDeletedOutboxRepository(dbConnection, transaction);
    }

    public IFileMetadataDeletedOutboxRepository Create()
    {
        var connection = connectionFactory.Create();
        return new FileMetadataDeletedOutboxRepository(connection);
    }
}
