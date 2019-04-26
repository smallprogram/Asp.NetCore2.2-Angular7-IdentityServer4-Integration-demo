using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
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
using System.Linq;

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
                //options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());

                //配置core使用自定义的媒体类型作为传入数据
                var inputFormatter = options.InputFormatters.OfType<JsonInputFormatter>().FirstOrDefault();
                if(inputFormatter != null)
                {
                    inputFormatter.SupportedMediaTypes.Add("application/vnd.smallprogram.post.create+json"); 
                    inputFormatter.SupportedMediaTypes.Add("application/vnd.smallprogram.post.update+json");
                }
                
                //配置core使用自定义的媒体类型返回数据
                var outputFormatters = options.OutputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();
                if (outputFormatters != null)
                {
                    outputFormatters.SupportedMediaTypes.Add("application/vnd.smallprogram.hateoas+json");
                }
                //options.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
            })
            .AddJsonOptions(options =>
            {
                //设置返回的Json中属性名称为小写字母
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            })
            .AddFluentValidation();//配置Asp.net Core启用FluentValidation验证

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
                options.HttpsPort = 6001;
            });

            //注册Respository
            services.AddScoped<IPostRepository, PostRepository>();
            //注册UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //注册映射服务
            services.AddAutoMapper();

            //注册FluentValidat验证器
            services.AddTransient<IValidator<PostAddResource>, PostAddOrUpdateResourceValidator<PostAddResource>>();
            services.AddTransient<IValidator<PostUpdateResource>, PostAddOrUpdateResourceValidator<PostUpdateResource>>();

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


            #region 配置IdentityServer4的AccessToken验证

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:5001"; //授权地址配置为Idp的地址
                    options.ApiName = "RESTApi";   //授权ApiName配置为Idp中配置的Scopes里的ApiName
                });

            #endregion

            //配置允许跨域请求
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDevOrigin", builder =>
                 builder.WithOrigins("http://localhost:4200")
                 .WithExposedHeaders("X-Pagination") //允许自定义header
                 .AllowAnyHeader()
                 .AllowAnyMethod());
            });


            #region 配置所有控制器访问策略

            services.Configure<MvcOptions>(options =>
            {
                //配置跨域过滤器
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAngularDevOrigin"));

                //配置策略，所有控制器只有经过身份验证的用户可以访问
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                //添加策略
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            #endregion


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseDeveloperExceptionPage();
            //使用自定义的错误处理器管道
            app.UseMyExceptionHandler(loggerFactory);

            app.UseCors("AllowAngularDevOrigin");
            
            //https重定向
            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
