using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDoListBS.Filters
{
    public class ResultFilter : IResultFilter, IAsyncResultFilter
    {
        // 同步方法 - 執行完成後
        public void OnResultExecuted(ResultExecutedContext context)
        {
          //  context.HttpContext.Response.WriteAsync("ResultFilter is executed. \r\n");
        }

        // 同步方法 - 執行前
        public void OnResultExecuting(ResultExecutingContext context)
        {
           // context.HttpContext.Response.WriteAsync("Executing ResultFilter. \r\n");
        }

        // 非同步方法 - 執行中
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            //await context.HttpContext.Response.WriteAsync("AsyncActionFilter AsyncResultFilter. \r\n");
            //await next();
            //await context.HttpContext.Response.WriteAsync("AsyncResultFilter is executed. \r\n");
        }
    }
}
