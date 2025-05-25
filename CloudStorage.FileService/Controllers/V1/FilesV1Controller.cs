using CloudStorage.FileService.Domain.FileManagement.ValueObjects;
using CloudStorage.UseCases;
using CloudStorage.UseCases.DeleteFile;
using CloudStorage.UseCases.GetFile;
using CloudStorage.UseCases.UploadFile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudStorage.FilesService.Controllers.V1;

[ApiController]
[Route("/api/v1/files")]
[Produces("application/json")]
public class FilesV1Controller(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetFile(Guid id, CancellationToken cancellationToken)
    {
        var command = new GetFileQuery(new FileMetadataId(id));
        var result = await mediator.Send(command, cancellationToken);
        return File(result.FileStream, result.FileMetadataDto.MimeType, result.FileMetadataDto.FileName);
    }

    [HttpPost]
    [RequestSizeLimit(100_000_000)]
    [ProducesResponseType<UploadFileCommandResult>(StatusCodes.Status202Accepted)]
    [Authorize]
    public async Task<IResult> UploadFile(IFormFile file)
    {
        await using var memoryStream = new MemoryStream((int)file.Length);
        await file.CopyToAsync(memoryStream);
        var fileMetadata = new FileMetadataDto(file.FileName, file.Length, file.ContentType);
        var command = new UploadFileCommand(fileMetadata, memoryStream);
        var uploadFileCommandResult = await mediator.Send(command);
        return Results.Accepted($"/api/v1/files/{uploadFileCommandResult.FileMetadataId}",uploadFileCommandResult);
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task DeleteFile(Guid id)
    {
        var command = new DeleteFileCommand(new FileMetadataId(id));
        await mediator.Send(command);
    }
}
