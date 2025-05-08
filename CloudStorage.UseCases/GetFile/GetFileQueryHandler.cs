using CloudStorage.Domain.Abstractions;
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
        await using var fileStream = await fileStorage.DownloadFileAsync(storageId, cancellationToken);
        throw new NotImplementedException();
    }
}
