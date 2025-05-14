using System.Data;

namespace CloudStorage.Domain.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IFileMetadataRepository FileMetadataRepository { get; }

    IFileMetadataDeletedOutboxRepository FileMetadataDeletedOutboxRepository { get; }

    Task BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default);
    
    Task CommitAsync(CancellationToken cancellationToken = default);

    Task RollbackAsync(CancellationToken cancellationToken = default);
}
