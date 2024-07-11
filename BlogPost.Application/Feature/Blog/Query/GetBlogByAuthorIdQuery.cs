// ReSharper disable ClassNeverInstantiated.Global
namespace BlogPost.Application.Feature.Blog.Query;

public class GetBlogByAuthorIdQuery : IRequest<Result<List<BlogResponseDto>>>
{
    public uint AuthorId { get; set; }
    public int PageNumber { get; set; }
    public GetBlogByAuthorIdQuery(uint authorId, int pageNumber)
    {
        AuthorId = authorId;
        PageNumber = pageNumber;
    }
}



public class GetBlogByAuthorIdHandler : IRequestHandler<GetBlogByAuthorIdQuery, Result<List<BlogResponseDto>>>
{
    private readonly IBlogRepository _blogRepository;
    private readonly ILogger _logger;

    public GetBlogByAuthorIdHandler(ILogger logger, IBlogRepository blogRepository)
    {
        _logger = logger;
        _blogRepository = blogRepository;
    }

    public async Task<Result<List<BlogResponseDto>>> Handle(GetBlogByAuthorIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.PageNumber <= 0)
            {
                request.PageNumber = 1;
            }

            const int pageSize = 50;
            var blog = await _blogRepository.GetAllBlogByAuthorId(request.AuthorId, request.PageNumber, pageSize); 
            if (blog.Count == 0)
            {
                return Helper.ListResponse(new List<BlogResponseDto>(), "00", "No record found.", 
                    $"No blog record found for author with id {request.AuthorId}" ,_logger);
            }

            var response = new Result<List<BlogResponseDto>>
            {
                ResponseCode = "00",
                ResponseDetails = blog.BlogList,
                ResponseMsg = "Record retrieve successfully.",
                PaginationDetails = new PaginationDetails
                {
                    PageSize = pageSize,
                    PageNumber = request.PageNumber,
                    TotalRecords = blog.Count
                }
            };

            return response;
        }
        catch (Exception ex)
        {
            return Helper.ListResponse(new List<BlogResponseDto>(), "50", "An error occurred, please check your internet and try again.", 
                $"An error occurred. Error ==> {ex.Message}; stackTrace ==> {ex.StackTrace}" ,_logger);
        }
    }
}



