using CloudStorage.Domain.Entities.Ids;
using MediatR;

namespace CloudStorage.UseCases.GetFile;

public record GetFileQuery(FileMetadataId FileMetadataId) : IRequest<GetFileQueryResult>;
