using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDoListBS.Filters
{
    public class ResourceFilter : IResourceFilter, IAsyncResourceFilter
    {
        // 同步方法 - 執行完成後
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //context.HttpContext.Response.WriteAsync("Executed ResourceFilter. \r\n");
        }

        // 同步方法 - 執行前
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //context.HttpContext.Response.WriteAsync("Executing ResourceFilter. \r\n");
        }

        // 非同步方法 - 執行中
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            //await context.HttpContext.Response.WriteAsync("Executing AsyncResouceFilter. \r\n");
            //await next();
            //await context.HttpContext.Response.WriteAsync("AsyncResouceFilter is executed. \r\n");
        }


    }
}
