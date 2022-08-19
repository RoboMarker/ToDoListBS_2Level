using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDoListBS.Filters
{
    public class ExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {
        // 同步方法
        public void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.WriteAsync("Executing ExceptionFilter. \r\n");
        }

        // 非同步方法
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            await context.HttpContext.Response.WriteAsync("AsyncExceptionFilter ExceptionFilter. \r\n");
        }
    }
}
