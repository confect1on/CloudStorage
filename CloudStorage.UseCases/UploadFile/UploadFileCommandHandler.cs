using CloudStorage.Domain.Abstractions;
using CloudStorage.Domain.FileManagement;
using CloudStorage.Domain.FileManagement.Entities;
using CloudStorage.Domain.FileManagement.Repositories;
using CloudStorage.Domain.FileManagement.ValueObjects;
using MediatR;

namespace CloudStorage.UseCases.UploadFile;

internal sealed class UploadFileCommandHandler(
    IFileMetadataRepository fileMetadataRepository,
    IDateTimeOffsetProvider dateTimeOffsetProvider,
    IUserAccessor userAccessor,
    IFileStorage fileStorage) : IRequestHandler<UploadFileCommand, UploadFileCommandResult>
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
            UserId = userAccessor.GetCurrentUserId(),
        };
        var fileMetadataId = await fileMetadataRepository.AddAsync(fileMetadata, cancellationToken);
        // TODO: add checking StorageQuota
        // TODO: replace by outbox-pattern
        var storageId = await fileStorage.UploadFile(request.FileStream, cancellationToken);
        await fileMetadataRepository.AttachStorageIdAsync(fileMetadataId, storageId, cancellationToken);
        return new UploadFileCommandResult(fileMetadataId);
    }
}
