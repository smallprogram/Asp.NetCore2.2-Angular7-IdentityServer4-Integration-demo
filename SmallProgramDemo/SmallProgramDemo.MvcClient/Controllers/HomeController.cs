using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using SmallProgramDemo.MvcClient.Models;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SmallProgramDemo.MvcClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            var idToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            ViewBag.accessToken = accessToken;
            ViewBag.idToken = idToken;
            ViewBag.refreshToken = refreshToken;

            return View();
        }

        public async Task<IActionResult> Posts()
        {
            //配置访问ApiResource的地址
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:6001")
            };
            //配置请求的Accept媒体类型
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.smallprogram.hateoas+json")
            );

            //获取当前认证之后的AccessToken
            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            //存到ViewData中可以在前台显示一下
            ViewData["accessToken"] = accessToken;
            //将请求中的BearerToken设置为AccessToken
            httpClient.SetBearerToken(accessToken);

            //使用配置好的httpClient访问ApiResource资源
            var res = await httpClient.GetAsync("api/posts").ConfigureAwait(false);

            //访问成功
            if (res.IsSuccessStatusCode)
            {
                //取出响应的content
                var json = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
                //将数据转换为dynamic格式
                var objects = JsonConvert.DeserializeObject<dynamic>(json);
                //存入ViewData以备前台显示
                ViewData["json"] = objects;
                //返回视图
                return View();
            }
            //如果响应失败
            if (res.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("AccessDenied", "Authorization");
            }

            throw new Exception($"Error Occurred: ${res.ReasonPhrase}");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
