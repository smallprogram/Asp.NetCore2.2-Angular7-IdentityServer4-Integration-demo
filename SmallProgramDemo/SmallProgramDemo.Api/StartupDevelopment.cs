using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmallProgramDemo.Api.Extensions;
using SmallProgramDemo.Core.Interface;
using SmallProgramDemo.Infrastructure.Database;
using SmallProgramDemo.Infrastructure.Repository;

namespace SmallProgramDemo.Api
{
    public class StartupDevelopment
    {
        private readonly IConfiguration configuration;

        public StartupDevelopment(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //获取当前机器名称
            var MachineName = System.Environment.MachineName;
            var ConnectionsString = "";
            if (MachineName == "CGYYPC") //如果时单位机器
            {
                ConnectionsString = configuration.GetConnectionString("CgyyConnection");
                //ConnectionsString = configuration["ConnectionStrings:CgyyConnection"];
            }
            else
            {
                ConnectionsString = configuration.GetConnectionString("HomeConnection");
                //ConnectionsString = configuration["ConnectionStrings:HomeConnection"];
            }

            services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(ConnectionsString);
            });

            //https重定向
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });

            //注册Respository
            services.AddScoped<IPostRepository, PostRepository>();
            //注册UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseDeveloperExceptionPage();
            //使用自定义的错误处理器管道
            app.UseMyExceptionHandler(loggerFactory);

            //https重定向
            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
