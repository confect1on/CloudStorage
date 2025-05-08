using CloudStorage.Domain.Entities.Ids;
using MediatR;

namespace CloudStorage.UseCases.DeleteFile;

public record DeleteRecordCommand(FileMetadataId FileMetadataId) : IRequest;
