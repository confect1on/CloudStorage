using System.Transactions;
using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.Entities;
using CloudStorage.Domain.Entities.Ids;
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
            var fileMetadataDeletedOutbox = new FileMetadataOutbox(
                EventId.New(),
                request.FileMetadataId,
                OutboxStatus.Pending,
                dateTimeOffsetProvider.GetUtcNow());
            await unitOfWork.FileMetadataDeletedOutboxRepository.AddAsync(fileMetadataDeletedOutbox, cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken);
        }
        catch
        {
            await unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
