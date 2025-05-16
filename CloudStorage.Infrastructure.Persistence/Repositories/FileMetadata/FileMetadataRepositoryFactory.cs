using System.Data;
using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.FileManagement;
using CloudStorage.Infrastructure.Persistence.ConnectionFactory;

namespace CloudStorage.Infrastructure.Persistence.Repositories.FileMetadata;

internal sealed class FileMetadataRepositoryFactory(
    IDateTimeOffsetProvider dateTimeOffsetProvider,
    IConnectionFactory connectionFactory) : IRepositoryFactory<IFileMetadataRepository>
{
    public IFileMetadataRepository Create(IDbConnection dbConnection, IDbTransaction? transaction = null)
    {
        return new FileMetadataRepository(
            dateTimeOffsetProvider,
            dbConnection,
            transaction);
    }

    public IFileMetadataRepository Create()
    {
        var connection = connectionFactory.Create();
        return new FileMetadataRepository(dateTimeOffsetProvider, connection);
    }
}
