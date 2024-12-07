using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using BuberDinner.Application.Common.Errors;

namespace BuberDinner.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features
                               .Get<IExceptionHandlerFeature>()?.Error;
        //cau truc switch case nhung tra ve ket qua
        var (statusCode, message) = exception switch
        {
            //IServiceException serviceException => ((int) 
            //    serviceException.StatusCode,
            //    serviceException.ErrorMessage),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
        };
        return Problem(statusCode: statusCode, title: message);
    }   
}
