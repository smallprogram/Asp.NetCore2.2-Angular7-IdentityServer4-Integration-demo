using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SmallProgramDemo.Core.Entities;
using SmallProgramDemo.Core.Interface;
using SmallProgramDemo.Infrastructure.Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmallProgramDemo.Api.Controllers
{
    [Route("api/posts")]
    public class PostController : Controller
    {
        private readonly IPostRepository postRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<PostController> logger;
        private readonly IMapper mapper;
        private readonly IUrlHelper urlHelper;

        public PostController(
            IPostRepository postRepository,
            IUnitOfWork unitOfWork,
            ILogger<PostController> logger,
            IMapper mapper,
            IUrlHelper urlHelper)
        {
            this.postRepository = postRepository;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
            this.urlHelper = urlHelper;
        }


        [HttpGet(Name ="getPosts")]
        public async Task<IActionResult> Get(PostQueryParameters postQueryParameters)
        {
            //返回带有元数据的PaginatedList数据
            var postsWithMateData = await postRepository.GetAllPosts(postQueryParameters);
            //将posts集合映射为PostResource集合，取出其中的post数据集合
            var postResource = mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(postsWithMateData);

            var previousPageLink = postsWithMateData.HasPrevious ?
                CreatePostUri(postQueryParameters, PaginationResourceUriType.PreviousPage,urlHelper, "getPosts") : null;

            var nextPageLink = postsWithMateData.HasNext ?
                CreatePostUri(postQueryParameters, PaginationResourceUriType.NextPage, urlHelper, "getPosts") : null;

            //提取元数据
            var mate = new
            {
                PageSize = postsWithMateData.PageSize,
                PageIndex = postsWithMateData.PageIndex,
                TotalItemCount = postsWithMateData.TotalItemsCount,
                PageCount = postsWithMateData.PageCount,
                previousPageLink,
                nextPageLink
            };

            //将翻页元数据添加到响应Header的X-Pagination中
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(mate, new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver() //将所有mate元数据属性首字母小写返回
            }));

            //logger.LogError("测试的错误日志记录");
            //throw new Exception("发生了错误");
            return Ok(postResource);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await postRepository.GetPostById(id);

            //处理没找到的情况返回404
            if (post == null)
            {
                return NotFound();
            }

            var postResource = mapper.Map<Post, PostResource>(post);

            return Ok(postResource);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var post = new Post
            {
                Title = "Post Title add",
                Body = "Post Body add",
                Author = "zhusir",
                LastModified = DateTime.Now
            };
            postRepository.AddPost(post);
            await unitOfWork.SaveAsync();
            return Ok();
        }



        /// <summary>
        /// 生成前一页，后一页，当前页的URI链接方法
        /// </summary>
        /// <param name="parameters">页面的元数据</param>
        /// <param name="uriType">URI类型</param>
        /// <param name="_urlHelper">URIHelper</param>
        /// <param name="actionName">需要调用的ActionName</param>
        /// <returns></returns>
        private string CreatePostUri(PostQueryParameters parameters, PaginationResourceUriType uriType, IUrlHelper _urlHelper, string actionName)
        {
            switch (uriType)
            {
                case PaginationResourceUriType.PreviousPage:
                    var previousParameters = new
                    {
                        pageIndex = parameters.PageIndex - 1,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields
                    };
                    return _urlHelper.Link(actionName, previousParameters);
                case PaginationResourceUriType.NextPage:
                    var nextParameters = new
                    {
                        pageIndex = parameters.PageIndex + 1,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields
                    };
                    return _urlHelper.Link(actionName, nextParameters);
                default:
                    var currentParameters = new
                    {
                        pageIndex = parameters.PageIndex,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields
                    };
                    return _urlHelper.Link(actionName, currentParameters);
            }
        }
    }
}