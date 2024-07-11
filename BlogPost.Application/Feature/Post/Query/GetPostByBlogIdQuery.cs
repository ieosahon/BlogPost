// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace BlogPost.Application.Feature.Post.Query;

public class GetPostByBlogIdQuery : IRequest<Result<List<PostResponseDto>>>
{
    public uint BlogId { get; set; }
    public int PageNumber { get; set; }
    public GetPostByBlogIdQuery(uint blogId, int pageNumber)
    {
        BlogId = blogId;
        PageNumber = pageNumber;
    }
}

public class GetPostByBlogIdHandler : IRequestHandler<GetPostByBlogIdQuery, Result<List<PostResponseDto>>>
{
    private readonly IPostRepository _postRepository;
    private readonly ILogger _logger;

    public GetPostByBlogIdHandler(ILogger logger, IPostRepository postRepository)
    {
        _logger = logger;
        _postRepository = postRepository;
    }
    public async Task<Result<List<PostResponseDto>>> Handle(GetPostByBlogIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.PageNumber <= 0)
            {
                request.PageNumber = 1;
            }

            const int pageSize = 50;
            var post = await _postRepository.GetAllPostByBlogId(request.BlogId, request.PageNumber, pageSize); 
            if (post.Count == 0)
            {
                return Helper.ListResponse(new List<PostResponseDto>(), "00", "No record found.", 
                    $"No post record found for blog with id {request.BlogId}" ,_logger);
            }

            var response = new Result<List<PostResponseDto>>
            {
                ResponseCode = "00",
                ResponseDetails = post.PostList,
                ResponseMsg = "Record retrieve successfully.",
                PaginationDetails = new PaginationDetails
                {
                    PageSize = pageSize,
                    PageNumber = request.PageNumber,
                    TotalRecords = post.Count
                }
            };

            return response;
        }
        catch (Exception ex)
        {
            return Helper.ListResponse(new List<PostResponseDto>(), "50", "An error occurred, please check your internet and try again.", 
                $"An error occurred. Error ==> {ex.Message}; stackTrace ==> {ex.StackTrace}" ,_logger);
        }
    }
}