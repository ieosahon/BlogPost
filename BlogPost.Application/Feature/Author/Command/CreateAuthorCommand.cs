global using BlogPost.Application.Contract.Interface;
global using BlogPost.Domain.DTOs.Request;
global using BlogPost.Domain.DTOs.Response;
global using FluentValidation;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Serilog;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace BlogPost.Application.Feature.Author.Command;

public class CreateAuthorCommand : IRequest<Result<string>>
{
    public AuthorDto Request { get; set; }
    public CreateAuthorCommand(AuthorDto request)
    {
        Request = request;
    }
}

public class CreateAuthorValidatorAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("request", out var requestObject) && requestObject is AuthorDto request)
        {
            var validator = new CreateAuthorValidator();
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
public class CreateAuthorValidator : AbstractValidator<AuthorDto>
{
    public CreateAuthorValidator()
    {
        RuleFor(x => x.FirstName)
            .Cascade(CascadeMode.Stop)
            .MinimumLength(3)
            .MaximumLength(100)
            .WithMessage("FirstName must be of minimum length of 3 and a maximum length of 100 characters.");
        RuleFor(x => x.LastName)
            .Cascade(CascadeMode.Stop)
            .MinimumLength(3)
            .MaximumLength(100)
            .WithMessage("LastName must be of minimum length of 3 and a maximum length of 100 characters.");
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required.")
            .Must(Helper.EmailValidator.IsValidEmail!).WithMessage("Invalid email address.");
    }
}

public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    public CreateAuthorHandler(IUnitOfWork unitOfWork, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<Result<string>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingAuthor = await _unitOfWork.Repository<Domain.Entity.Author>().GetSingle(x => x.Email == request.Request.Email);
            if (existingAuthor is not null)
            {
                return Helper.LogAndCreateResult("49", $"Author with email: {request.Request.Email} already exists.",
                    $"Author with email : {request.Request.Email} already exist.", _logger);
            }

            var author = new Domain.Entity.Author
            {
                Email = request.Request.Email,
                FirstName = request.Request.FirstName,
                LastName = request.Request.LastName
            };
            _unitOfWork.Repository<Domain.Entity.Author>().Add(author);

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) < 1)
            {
                return Helper.LogAndCreateResult("50", "An error occurred, please check your internet and try again.",
                    $"Unable to save author with email;  {request.Request.Email}.", _logger);
            }

            return Helper.ResponseMessage("00", "Successfully created author.");
        }
        catch (Exception ex)
        {
              return Helper.LogAndCreateResult("50", "An error occurred, please check your internet and try again.",
                    $"An error occurred. Error ==> {ex.Message}; stackTrace ==> {ex.StackTrace}", _logger);
            
        }
    }
}