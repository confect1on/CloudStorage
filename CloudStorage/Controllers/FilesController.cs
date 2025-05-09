using CloudStorage.Domain.Entities.Ids;
using CloudStorage.UseCases.GetFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudStorage.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class FilesController(IMediator mediator)
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetFile(Guid id, CancellationToken cancellationToken)
    {
        var command = new GetFileQuery(new FileMetadataId(id));
        var result = await mediator.Send(command, cancellationToken);
        throw new NotImplementedException();
    }

    [HttpPost]
    public Task UploadFile(IFormFile file)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:guid}")]
    public Task DeleteFile(Guid id)
    {
        throw new NotImplementedException();
    }
}
