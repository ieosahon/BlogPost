// ReSharper disable ClassNeverInstantiated.Global
namespace BlogPost.Application.Feature.Author.Query;

public class GetAllAuthorsQuery : IRequest<Result<List<AuthorResponseDto>>>
{
    public GetAllAuthorsQuery(int pageNumber)
    {
        PageNumber = pageNumber;
    }

    public int PageNumber { get; set; }
}


public class GetAllAuthorIdHandler : IRequestHandler<GetAllAuthorsQuery, Result<List<AuthorResponseDto>>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly ILogger _logger;

    public GetAllAuthorIdHandler(ILogger logger, IAuthorRepository authorRepository)
    {
        _logger = logger;
        _authorRepository = authorRepository;
    }

    public async Task<Result<List<AuthorResponseDto>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.PageNumber <= 0)
            {
                request.PageNumber = 1;
            }

            const int pageSize = 50;
            var authors = await _authorRepository.GetAllAuthors(request.PageNumber, pageSize); 
            if (authors.Count == 0)
            {
                return Helper.ListResponse(new List<AuthorResponseDto>(), "00", "No record found.", 
                    $"No author record found." ,_logger);
            }

            var response = new Result<List<AuthorResponseDto>>
            {
                ResponseCode = "00",
                ResponseDetails = authors.AuthorList,
                ResponseMsg = "Record retrieve successfully.",
                PaginationDetails = new PaginationDetails
                {
                    PageSize = pageSize,
                    PageNumber = request.PageNumber,
                    TotalRecords = authors.Count
                }
            };

            return response;
        }
        catch (Exception ex)
        {
            return Helper.ListResponse(new List<AuthorResponseDto>(), "50", "An error occurred, please check your internet and try again.", 
                $"An error occurred. Error ==> {ex.Message}; stackTrace ==> {ex.StackTrace}" ,_logger);
        }
    }}