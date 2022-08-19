using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDoListBS.Filters
{
    public class AuthorFilter:IAuthorizationFilter,IAsyncAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //context.HttpContext.Response.WriteAsync("Executing AuthorizationFilter. \r\n");
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //await context.HttpContext.Response.WriteAsync("Executing AsyncAuthorizationFilter. \r\n");
        }
    }
}
