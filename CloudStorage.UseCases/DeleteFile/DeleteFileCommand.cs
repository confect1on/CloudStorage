using CloudStorage.Domain.Entities.Ids;
using MediatR;

namespace CloudStorage.UseCases.DeleteFile;

public record DeleteFileCommand(FileMetadataId FileMetadataId) : IRequest;
