using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.FileManagement.Entities;
using CloudStorage.Domain.FileManagement.ValueObjects;
using MediatR;

namespace CloudStorage.UseCases.DeleteFile;

internal sealed class DeleteFileCommandHandler(
    IUnitOfWork unitOfWork,
    IDateTimeOffsetProvider dateTimeOffsetProvider) : IRequestHandler<DeleteFileCommand>
{
    public async Task Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken: cancellationToken);
        try
        {
            await unitOfWork.FileMetadataRepository.DeleteByIdAsync(request.FileMetadataId, cancellationToken);
            var fileMetadataDeletedOutbox = new FileManagementOutbox(
                FileManagementOutboxId.New(),
                request.FileMetadataId,
                FileManagementOutboxStatus.Pending,
                dateTimeOffsetProvider.GetUtcNow());
            await unitOfWork.FileManagementOutboxRepository.AddAsync(fileMetadataDeletedOutbox, cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken);
        }
        catch
        {
            await unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
