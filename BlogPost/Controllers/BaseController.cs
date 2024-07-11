global using BlogPost.Domain.DTOs.Response;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;

// ReSharper disable NotAccessedField.Local

namespace BlogPost.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
public class BaseController : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    protected readonly IMediator Mediator;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public BaseController(IMediator mediator)
    {
        Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    internal IActionResult HandleResponse<T>(Result<T> result)
    {
        return result.ResponseCode switch
        {
            "00" => Ok(result),
            "40" => BadRequest(result),
            "44" => NotFound(result),
            "49" => Conflict(result),
            _ => StatusCode(500, result)
        };
    }
}
