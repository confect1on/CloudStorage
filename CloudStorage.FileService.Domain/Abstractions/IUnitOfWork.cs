using System.Data;
using CloudStorage.FileService.Domain.FileManagement;
using CloudStorage.FileService.Domain.FileManagement.Repositories;
using CloudStorage.FileService.Domain.FileManagement.Repositories.FileManagementOutboxRepository;

namespace CloudStorage.FileService.Domain.Abstractions;

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
