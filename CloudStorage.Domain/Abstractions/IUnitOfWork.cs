using System.Data;
using CloudStorage.Domain.FileManagement;
using CloudStorage.Domain.FileManagement.Repositories.FileManagementOutboxRepository;

namespace CloudStorage.Domain.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IFileMetadataRepository FileMetadataRepository { get; }

    IFileManagementOutboxRepository FileManagementOutboxRepository { get; }

    Task BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default);
    
    Task CommitAsync(CancellationToken cancellationToken = default);

    Task RollbackAsync(CancellationToken cancellationToken = default);
}
