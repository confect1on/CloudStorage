using CloudStorage.FileService.Domain.FileManagement.ValueObjects;
using MediatR;

namespace CloudStorage.UseCases.GetFile;

public record GetFileQuery(FileMetadataId FileMetadataId) : IRequest<GetFileQueryResult>;
