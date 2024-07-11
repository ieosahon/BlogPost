// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
namespace BlogPost.Application.Feature.Post.Command;

public class CreatePostCommand : IRequest<Result<string>>
{
    
    public PostDto Request { get; set; }
    public CreatePostCommand(PostDto request)
    {
        Request = request;
    }
}
public class CreatePostValidatorAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("request", out var requestObject) && requestObject is PostDto request)
        {
            var validator = new CreatePostValidator();
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
public class CreatePostValidator : AbstractValidator<PostDto>
{
    public CreatePostValidator()
    {
        RuleFor(x => x.Content)
            .Cascade(CascadeMode.Stop)
            .MinimumLength(3)
            .MaximumLength(5000)
            .WithMessage("Content must be of minimum length of 3 and a maximum length of 5000 characters.");
        RuleFor(x => x.BlogId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .WithMessage("Blog is required.");
    }
}

public class CreatePostHandler : IRequestHandler<CreatePostCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public CreatePostHandler(IUnitOfWork unitOfWork, ILogger logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<string>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var blog = await _unitOfWork.Repository<Domain.Entity.Blog>().GetSingle(x => x.Id == request.Request.BlogId);
            if (blog is null)
            {
                return Helper.LogAndCreateResult("44", $"Blog post with Id: {request.Request.BlogId} does not exists.",
                    $"Blog post with Id: {request.Request.BlogId} does not exists.", _logger);
            }

            var post = new Domain.Entity.Post
            {
                Content = request.Request.Content,
                BlogId = request.Request.BlogId
            };
            _unitOfWork.Repository<Domain.Entity.Post>().Add(post);
            if ( await _unitOfWork.SaveChangesAsync(cancellationToken) < 1)
            {
                return Helper.LogAndCreateResult("50", "An error occurred, please check your internet and try again.",
                    $"Unable to save post with blog Id;  {request.Request.BlogId}.", _logger);
            }
            return Helper.ResponseMessage("00", "Successfully created post.");
        }
        catch (Exception ex)
        {
            return Helper.LogAndCreateResult("50", "An error occurred, please check your internet and try again.",
                $"An error occurred. Error ==> {ex.Message}; stackTrace ==> {ex.StackTrace}", _logger);
        }
    }
}