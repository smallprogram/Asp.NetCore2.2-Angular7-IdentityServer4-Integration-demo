using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmallProgramDemo.Core.Interface;
using SmallProgramDemo.Infrastructure.Database;
using SmallProgramDemo.Infrastructure.Repository;

namespace SmallProgramDemo.Api
{
    public class StartupDevelopment
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer("data source=192.159.93.130;initial catalog=SmallProgramDemoDB;persist security info=True;user id=sa;password=sa;MultipleActiveResultSets=True;");
            });

            //https重定向
            services.AddHttpsRedirection(options => {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;
            });

            //注册Respository
            services.AddScoped<IPostRepository, PostRepository>();
            //注册UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            //https重定向
            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
