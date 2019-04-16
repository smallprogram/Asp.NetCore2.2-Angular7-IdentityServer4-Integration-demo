using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SmallProgramDemo.Api.Extensions
{
    //自定义的错误处理器管道
    public static class ExceptionHandlingExtensions
    {
        public static void UseMyExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    //配置返回的状态码为500
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    //配置返回的响应内容格式为JSON格式
                    context.Response.ContentType = "application/json";

                    //获取错误内容
                    var ex = context.Features.Get<IExceptionHandlerFeature>();

                    if (ex != null)
                    {
                        var logger = loggerFactory.CreateLogger("SmallProgramDemo.Api.Extensions.ExceptionHandlingExtensions");
                        logger.LogError(500, ex.Error, ex.Error.Message);
                    }

                    await context.Response.WriteAsync(ex?.Error?.Message ?? "发生了一个错误，被UseMyExceptionHandler捕获");
                });
            });
        }
    }
}
