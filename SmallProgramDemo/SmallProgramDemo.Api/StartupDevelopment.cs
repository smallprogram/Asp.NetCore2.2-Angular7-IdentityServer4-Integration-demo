using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using SmallProgramDemo.Api.Extensions;
using SmallProgramDemo.Core.Interface;
using SmallProgramDemo.Infrastructure.Database;
using SmallProgramDemo.Infrastructure.Repository;
using SmallProgramDemo.Infrastructure.Resources;
using SmallProgramDemo.Infrastructure.Services;

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
            services.AddMvc(options =>
            {
                //配置内容协商不成功时返回406
                options.ReturnHttpNotAcceptable = true;
                //配置内容协商使服务器返回资源支持xml格式
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());

                //options.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
            })
            .AddJsonOptions(options =>
            {
                //设置返回的Json中属性名称为小写字母
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

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

            //注册映射服务
            services.AddAutoMapper();

            //注册FluentValidat验证器
            services.AddTransient<IValidator<PostResource>, PostResourceValidator>();

            //注册配置URI Helper
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();  //单例的ActionContextAccessor依赖注入
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            //注册配置Resource到Entity的映射，用于排序使用
            var propertyMappingContainer = new PropertyMappingContainer();
            propertyMappingContainer.Register<PostPropertyMapping>();
            services.AddSingleton<IPropertyMappingContainer>(propertyMappingContainer);

            //注册配置ResourceModel塑形属性合法性判断服务
            services.AddTransient<ITypeHelperService, TypeHelperService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            //使用自定义的错误处理器管道
            //app.UseMyExceptionHandler(loggerFactory);

            //https重定向
            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
