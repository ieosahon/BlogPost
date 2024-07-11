// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace BlogPost.Application.Feature.Blog.Command;

public class CreateBlogCommand : IRequest<Result<string>>
{
    public BlogDto Request { get; set; }
    
    public CreateBlogCommand(BlogDto request)
    {
        Request = request;
    }
}

public class CreateBlogValidatorAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("request", out var requestObject) && requestObject is BlogDto request)
        {
            var validator = new CreateBlogValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                context.Result = new BadRequestObjectResult(errorMessages);
                return;
            }
        }

        base.OnActionExecuting(context);
    }
}
public class CreateBlogValidator : AbstractValidator<BlogDto>
{
    public CreateBlogValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .MinimumLength(3)
            .MaximumLength(100)
            .WithMessage("FirstName must be of minimum length of 3 and a maximum length of 100 characters.");
        RuleFor(x => x.AuthorId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Author is required.");
    }
}

public class CreateBlogHandler : IRequestHandler<CreateBlogCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public CreateBlogHandler(IUnitOfWork unitOfWork, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var author = await _unitOfWork.Repository<Domain.Entity.Author>().GetSingle(x => x.Id == request.Request.AuthorId);
            if (author is null)
            {
                return Helper.LogAndCreateResult("44", $"Author with Id : {request.Request.AuthorId} not found.",
                    $"Author with Id : {request.Request.AuthorId} not found.", _logger);
            }

            var blog = await _unitOfWork.Repository<Domain.Entity.Blog>().GetSingle(x => x.Name == request.Request.Name);
            if (blog is not null)
            {
                return Helper.LogAndCreateResult("49", $"Blog post with name: {request.Request.Name} already exists.",
                    $"Blog post with name: {request.Request.Name} already exists.", _logger);
            }

            var path = DateTime.Now.Ticks.ToString();
            var newBlog = new Domain.Entity.Blog
            {
                Name = request.Request.Name,
                AuthorId = request.Request.AuthorId,
                Url = $"{request.Request.Name}/{path}"
            };
            _unitOfWork.Repository<Domain.Entity.Blog>().Add(newBlog);
            if ( await _unitOfWork.SaveChangesAsync(cancellationToken) < 1)
            {
                return Helper.LogAndCreateResult("50", "An error occurred, please check your internet and try again.",
                    $"Unable to save blog post with name;  {request.Request.Name}.", _logger);
            }
            return Helper.ResponseMessage("00", "Successfully created blog.");
        }
        catch (Exception ex)
        {
            return Helper.LogAndCreateResult("50", "An error occurred, please check your internet and try again.",
                $"An error occurred. Error ==> {ex.Message}; stackTrace ==> {ex.StackTrace}", _logger);
        }
    }
}
