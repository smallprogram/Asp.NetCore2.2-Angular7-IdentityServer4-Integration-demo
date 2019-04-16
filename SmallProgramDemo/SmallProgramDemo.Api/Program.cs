using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using SmallProgramDemo.Infrastructure.Database;

namespace SmallProgramDemo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //serilog配置
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()  //最小日志输入级别
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information) //覆盖带有Microsoft的日志级别为Information
            .Enrich.FromLogContext()
            .WriteTo.Console()
            //.WriteTo.File(Path.Combine("logs", @"log.txt"), rollingInterval: RollingInterval.Day) //记录到文件，每天记录
            .CreateLogger();


            var host = CreateWebHostBuilder(args).Build();

            #region 执行数据库初始化Seed
            using (var scope = host.Services.CreateScope()) //获取服务的Scope
            {
                var services = scope.ServiceProvider;  //通过Scope获取服务提供商
                var loggerFactory = services.GetRequiredService<ILoggerFactory>(); //通过服务提供商获取日志工厂

                try
                {
                    var myContext = services.GetRequiredService<MyContext>(); //通过服务提供商获取MyContext
                    MyContextSeed.SeedAsync(myContext, loggerFactory).Wait(); //执行数据库Seed种子
                }
                catch (Exception e)
                {
                    var logger = loggerFactory.CreateLogger<Program>(); //使用日志工厂创建一个Program级别的日志记录实例
                    logger.LogError(e, "在数据库初始化Seed时发生了错误"); //记录错误日志
                }
            }
            #endregion


            host.Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseStartup<StartupProduction>();
                .UseStartup(typeof(StartupDevelopment).GetTypeInfo().Assembly.FullName)
                .UseSerilog();  //使用Serilog
    }
}
