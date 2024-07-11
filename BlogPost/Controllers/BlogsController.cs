global using BlogPost.Application.Feature.Blog.Command;
global using System.ComponentModel.DataAnnotations;
global using BlogPost.Application.Feature.Blog.Query;

namespace BlogPost.Controllers;

[Route("blog")]
public class BlogsController : BaseController
{
    public BlogsController(IMediator mediator) : base(mediator) { }
    
    [HttpPost]
    [CreateBlogValidator]
    public async Task<IActionResult> CreateAuthor([FromBody] BlogDto request)
        => HandleResponse(await Mediator.Send(new CreateBlogCommand(request))); 
    
    
    [HttpGet("{authorId}")]
    [CreateBlogValidator]
    public async Task<IActionResult> GetBlogs([Required]uint authorId , int pageNumber)
        => HandleResponse(await Mediator.Send(new GetBlogByAuthorIdQuery(authorId, pageNumber)));
}