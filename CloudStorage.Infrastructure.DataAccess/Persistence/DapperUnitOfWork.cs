using CloudStorage.Domain.Abstractions;
using CloudStorage.Infrastructure.DataAccess.Persistence.ConnectionFactory;
using CloudStorage.Infrastructure.DataAccess.Persistence.Repositories.FileMetadata;
using CloudStorage.Infrastructure.DataAccess.Persistence.Repositories.FileMetadataDeletedOutbox;
using Npgsql;
using IsolationLevel = System.Data.IsolationLevel;

namespace CloudStorage.Infrastructure.DataAccess.Persistence;

internal sealed class DapperUnitOfWork(
    IRepositoryFactory<IFileMetadataRepository> fileMetadataRepositoryFactory,
    IRepositoryFactory<IFileMetadataDeletedOutboxRepository> fileMetadataDeletedOutboxRepositoryFactory,
    IConnectionFactory connectionFactory) : IUnitOfWork, IAsyncDisposable
{
    private NpgsqlConnection? _connection;
    private NpgsqlTransaction? _transaction;

    public IFileMetadataRepository FileMetadataRepository =>
        fileMetadataRepositoryFactory.Create(
            _connection ?? throw new InvalidOperationException("Connection isn't initialized"),
            _transaction);
    
    public IFileMetadataDeletedOutboxRepository FileMetadataDeletedOutboxRepository => 
        fileMetadataDeletedOutboxRepositoryFactory.Create(
            _connection ?? throw new InvalidOperationException("Connection isn't initialized"),
            _transaction);
    
    public async Task BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default)
    {
        _connection ??= await connectionFactory.CreateAsync();
        _transaction = await _connection.BeginTransactionAsync(isolationLevel, cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("The transaction has not been started.");
        }
        await _transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("The transaction has not been started.");
        }
        await _transaction.RollbackAsync(cancellationToken);
    }

    public void Dispose()
    {
        _connection?.Dispose();
        _transaction?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection != null)
            await _connection.DisposeAsync();
        if (_transaction != null)
            await _transaction.DisposeAsync();
    }
}
