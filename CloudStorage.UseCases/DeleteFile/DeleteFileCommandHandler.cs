using CloudStorage.FileService.Domain;
using CloudStorage.FileService.Domain.Abstractions;
using CloudStorage.FileService.Domain.FileManagement.DomainEvents;
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
            var fileDeletedEvent = new FileDeletedEvent(EventId.New(), dateTimeOffsetProvider.GetUtcNow(), request.FileMetadataId);
            await unitOfWork.FileManagementOutboxRepository.AddAsync(fileDeletedEvent, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
        }
        catch
        {
            await unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
