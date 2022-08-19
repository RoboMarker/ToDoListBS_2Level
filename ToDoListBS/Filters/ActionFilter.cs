using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDoListBS.Filters
{
    public class ActionFilter : IActionFilter, IAsyncActionFilter
    {
        // 同步方法 - 執行完成後
        public void OnActionExecuted(ActionExecutedContext context)
        {
           // context.HttpContext.Response.WriteAsync("ActionFilter is executed. \r\n");
        }

        // 同步方法 - 執行前
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //context.HttpContext.Response.WriteAsync("Executing AsyncActionFilter. \r\n");
        }



        // 非同步方法 - 執行中
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //await context.HttpContext.Response.WriteAsync("AsyncActionFilter AsyncActionFilter. \r\n");
            //await next();
            //await context.HttpContext.Response.WriteAsync("AsyncActionFilter is executed. \r\n");
        }
    }
}
