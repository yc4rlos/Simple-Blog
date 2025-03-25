using Blog.Application.Core.UseCases.File.GetFile;

namespace Blog.Presentation.Web.Controllers;

[Route("[controller]")]
public class FileController(ISender sender): Controller
{
    [HttpGet("{fileName}")]
    public async Task<IActionResult> Index(GetFileQuery query)
    {
        var result = await sender.Send(query);
        
        return File(result.BlobObject.Content, result.BlobObject.ContentType, query.FileName);
    }
}