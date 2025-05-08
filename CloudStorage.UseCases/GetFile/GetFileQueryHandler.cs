using CloudStorage.Domain.Abstractions;
using CloudStorage.UseCases.Mappers;
using MediatR;
using FileNotFoundException = CloudStorage.Domain.Exceptions.FileNotFoundException;

namespace CloudStorage.UseCases.GetFile;

internal sealed class GetFileQueryHandler(
    IFileMetadataRepository fileMetadataRepository,
    IFileStorage fileStorage) : IRequestHandler<GetFileQuery, GetFileQueryResult>
{
    public async Task<GetFileQueryResult> Handle(GetFileQuery request, CancellationToken cancellationToken)
    {
        var fileMetadata = await fileMetadataRepository.GetByIdAsync(request.FileMetadataId, cancellationToken);
        var storageId = fileMetadata.StorageId ?? throw new FileNotFoundException(request.FileMetadataId);
        var fileStream = await fileStorage.DownloadFileAsync(storageId, cancellationToken);
        return new GetFileQueryResult(fileStream, fileMetadata.MapToFileMetadataDto());
    }
}
