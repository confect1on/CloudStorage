using System.Data;
using CloudStorage.FileService.Domain;
using CloudStorage.FileService.Domain.Abstractions;
using CloudStorage.FileService.Domain.FileManagement;
using CloudStorage.FileService.Domain.FileManagement.DomainEvents;
using CloudStorage.FileService.Domain.FileManagement.Entities;
using CloudStorage.FileService.Domain.FileManagement.Repositories;
using CloudStorage.FileService.Domain.FileManagement.Repositories.FileManagementOutboxRepository;
using CloudStorage.FileService.Domain.FileManagement.ValueObjects;
using MediatR;

namespace CloudStorage.UseCases.UploadFile;

internal sealed class UploadFileCommandHandler(
    IUnitOfWork uow,
    IDateTimeOffsetProvider dateTimeOffsetProvider,
    ICurrentUserAccessor currentUserAccessor,
    ITemporaryFileStorage temporaryFileStorage) : IRequestHandler<UploadFileCommand, UploadFileCommandResult>
{
    public async Task<UploadFileCommandResult> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var fileMetadata = new FileMetadata
        {
            Id = new FileMetadataId(Guid.NewGuid()),
            FileName = request.FileMetadataDto.FileName,
            MimeType = request.FileMetadataDto.MimeType,
            FileSizeInBytes = request.FileMetadataDto.FileSizeInBytes,
            CreatedAt = dateTimeOffsetProvider.GetUtcNow(),
            OwnerUserId = currentUserAccessor.GetCurrentUserId(),
        };
        await uow.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        try
        {
            // TODO: add checking StorageQuota
            var temporaryStorageId = await temporaryFileStorage.UploadFile(request.FileStream, cancellationToken);
            var fileMetadataId = await uow.FileMetadataRepository.AddAsync(fileMetadata, cancellationToken);
            var fileCreatedEvent = new FileCreatedEvent(
                EventId.New(),
                dateTimeOffsetProvider.GetUtcNow(),
                fileMetadataId,
                temporaryStorageId);
            await uow.FileManagementOutboxRepository.AddAsync(fileCreatedEvent, cancellationToken);
            await uow.CommitAsync(cancellationToken);
            return new UploadFileCommandResult(fileMetadataId);
        }
        catch (Exception)
        {
            await uow.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
