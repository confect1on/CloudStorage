using CloudStorage.Domain.Abstractions;
using MediatR;

namespace CloudStorage.UseCases.DeleteFile;

internal sealed class DeleteFileCommandHandler(
    IFileMetadataRepository fileMetadataRepository) : IRequestHandler<DeleteFileCommand>
{
    public async Task Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        // TODO: add validator that current user has rights to delete
        await fileMetadataRepository.DeleteByIdAsync(request.FileMetadataId, cancellationToken);
    }
}
