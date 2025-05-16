using System.Data;
using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.FileManagement.Repositories.FileManagementOutboxRepository;
using CloudStorage.Infrastructure.Persistence.ConnectionFactory;

namespace CloudStorage.Infrastructure.Persistence.Repositories.FileManagementOutbox;

internal sealed class FileManagementOutboxRepositoryFactory(
    IConnectionFactory connectionFactory,
    IDateTimeOffsetProvider dateTimeOffsetProvider) : IRepositoryFactory<IFileManagementOutboxRepository>
{
    public IFileManagementOutboxRepository Create(IDbConnection dbConnection, IDbTransaction? transaction = null)
    {
        return new FileManagementOutboxRepository(dateTimeOffsetProvider, dbConnection, transaction);
    }

    public IFileManagementOutboxRepository Create()
    {
        var connection = connectionFactory.Create();
        return new FileManagementOutboxRepository(dateTimeOffsetProvider, connection);
    }
}
