using CloudStorage.Domain.Abstractions;
using MediatR;

namespace CloudStorage.UseCases.DeleteFile;

internal sealed class DeleteRecordCommandHandler(
    IFileMetadataRepository fileMetadataRepository) : IRequestHandler<DeleteRecordCommand>
{
    public async Task Handle(DeleteRecordCommand request, CancellationToken cancellationToken)
    {
        // TODO: add validator that current user has rights to delete
        await fileMetadataRepository.DeleteByIdAsync(request.FileMetadataId, cancellationToken);
    }
}
