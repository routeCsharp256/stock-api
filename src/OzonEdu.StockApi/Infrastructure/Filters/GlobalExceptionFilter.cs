using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OzonEdu.StockApi.Infrastructure.Filters
{
    internal class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var resultObject = new
            {
                ExceptionType = context.Exception.GetType().FullName,
                Message = context.Exception.Message,
                StackTrace = context.Exception.StackTrace
            };
            var jsonResult = new JsonResult(resultObject)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.Result = jsonResult;
        }
    }
}