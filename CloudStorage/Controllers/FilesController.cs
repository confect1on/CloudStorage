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
    
    public 
}
