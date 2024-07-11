global using BlogPost.Application.Feature.Author.Command;
global using BlogPost.Domain.DTOs.Request;
using BlogPost.Application.Feature.Author.Query;

namespace BlogPost.Controllers;

[Route("author")]
public class AuthorController : BaseController
{
    public AuthorController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [CreateAuthorValidator]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto request)
        => HandleResponse(await Mediator.Send(new CreateAuthorCommand(request)));
    [HttpGet]
    public async Task<IActionResult> GetAllAuthors( int pageNumber)
        => HandleResponse(await Mediator.Send(new GetAllAuthorsQuery( pageNumber)));
}