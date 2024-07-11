global using BlogPost.Application.Feature.Post.Command;
global using BlogPost.Application.Feature.Post.Query;

namespace BlogPost.Controllers;

[Route("post")]
public class PostsController : BaseController
{
    public PostsController(IMediator mediator) : base(mediator) { }
    
    [HttpPost]
    [CreatePostValidator]
    public async Task<IActionResult> CreateAuthor([FromBody] PostDto request)
        => HandleResponse(await Mediator.Send(new CreatePostCommand(request)));
    
    [HttpGet("{blogId}")]
    [CreateBlogValidator]
    public async Task<IActionResult> GetBlogs([Required]uint blogId , int pageNumber)
        => HandleResponse(await Mediator.Send(new GetPostByBlogIdQuery(blogId, pageNumber)));
}