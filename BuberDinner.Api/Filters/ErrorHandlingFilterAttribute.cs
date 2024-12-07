using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace BuberDinner.Api.Filters;
//ExceptionFilterAttribute sử lý ngoại lệ 
//xử lý ngoại lệ cho toàn bộ project hoặc một sô controller/action
//khi một lỗi xảy ra Asp.Net core sẽ kiểm tra nó có apply bất kỳ bộ lọc ngoại lệ nào không
// => có chuyển quyền kiểm soát cho bộ lọc đó

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        var problemDetails = new ProblemDetails
        {
            Title = "An error occurred while processing your request",
            Instance = context.HttpContext.Request.Path,
            Status = (int) HttpStatusCode.InternalServerError,
            Detail = exception.Message
        };

        context.Result = new ObjectResult(problemDetails);

        context.ExceptionHandled = true;
    }
}
