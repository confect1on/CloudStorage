using Microsoft.AspNetCore.Mvc;

namespace CloudStorage.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class FilesController
{
    [HttpGet("{id:guid}")]
    public Task GetFile(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public Task UploadFile()
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:guid}")]
    public Task DeleteFile(Guid id)
    {
        throw new NotImplementedException();
    }
}
