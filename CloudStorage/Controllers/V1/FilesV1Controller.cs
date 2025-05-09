using CloudStorage.Domain.Entities.Ids;
using CloudStorage.UseCases.GetFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudStorage.Controllers.V1;

[ApiController]
[Route("/api/v1/files")]
public class FilesV1Controller(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetFile(Guid id, CancellationToken cancellationToken)
    {
        var command = new GetFileQuery(new FileMetadataId(id));
        var result = await mediator.Send(command, cancellationToken);
        throw new NotImplementedException();
    }

    [HttpPost]
    [RequestSizeLimit(100_000_000)]
    public async Task UploadFile(IFormFile file)
    {
        await using var memoryStream = new MemoryStream((int)file.Length);
        await file.CopyToAsync(memoryStream);
        throw new NotImplementedException();
    }

    [HttpDelete("{id:guid}")]
    public Task DeleteFile(Guid id)
    {
        throw new NotImplementedException();
    }
}
