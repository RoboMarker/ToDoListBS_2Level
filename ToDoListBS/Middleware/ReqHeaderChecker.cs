using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ToDoListBS.Middleware
{
    //自製middleware
    public class ReqHeaderChecker
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILoggerFactory _loggerFactory;
        public ReqHeaderChecker(RequestDelegate requestDelegate, ILoggerFactory loggerFactory)
        {
            this._requestDelegate = requestDelegate;
            this._loggerFactory = loggerFactory;
        }
        public async Task InvokeAsync(HttpContext context)
        { 
            var headers=context.Request.Headers;
            //string sHeaders = headers["super"].ToString();
            //
            //if (!sHeaders.Equals(@"test"))
            //{
            //    throw new Exception("Header Error!");
            //}
            await _requestDelegate.Invoke(context);
        }

        public async Task InvokeAsync2(HttpContext context)
        {
            try
            {
                await _requestDelegate.Invoke(context);
            }
            catch (Exception ex)
            {
                var logger = _loggerFactory.CreateLogger("ExceptionHandler");
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                var result = JsonConvert.SerializeObject(new { Result = "Error" });
                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
