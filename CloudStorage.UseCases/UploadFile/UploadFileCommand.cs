using MediatR;

namespace CloudStorage.UseCases.UploadFile;

public record UploadFileCommand(FileMetadataDto FileMetadataDto, Stream FileStream) : IRequest<UploadFileCommandResult>;
