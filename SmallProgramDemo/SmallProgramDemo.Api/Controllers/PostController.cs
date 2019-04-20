using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SmallProgramDemo.Api.Helpers;
using SmallProgramDemo.Core.Entities;
using SmallProgramDemo.Core.Interface;
using SmallProgramDemo.Infrastructure.Extensions;
using SmallProgramDemo.Infrastructure.Resources;
using SmallProgramDemo.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IPropertyMappingContainer propertyMappingContainer;
        private readonly ITypeHelperService typeHelperService;

        public PostController(
            IPostRepository postRepository,
            IUnitOfWork unitOfWork,
            ILogger<PostController> logger,
            IMapper mapper,
            IUrlHelper urlHelper,
            IPropertyMappingContainer propertyMappingContainer,
            ITypeHelperService typeHelperService)
        {
            this.postRepository = postRepository;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
            this.urlHelper = urlHelper;
            this.propertyMappingContainer = propertyMappingContainer;
            this.typeHelperService = typeHelperService;
        }

        #region Http方法

        #region 使用RequestHeaderMatchingMediaType Attribute进行判定执行哪种返回类型的Get方法
        [HttpGet(Name = "getPosts")]
        [RequestHeaderMatchingMediaType("Accept", new[] { "application/vnd.smallprogram.hateoas+json" })]
        public async Task<IActionResult> GetPostsHateoas(PostQueryParameters postQueryParameters)
        {
            #region 参数合法性判断
            //排序字段合法性判断
            if (!string.IsNullOrWhiteSpace(postQueryParameters.OrderBy) &&
                !propertyMappingContainer.ValidateMappingExistsFor<PostResource, Post>(postQueryParameters.OrderBy))
            {
                return BadRequest("排序属性映射关系不存在，或不可通过该排序属性排序");
            }
            //塑形字段合法性判断
            if (!string.IsNullOrWhiteSpace(postQueryParameters.Fields) &&
                !typeHelperService.TypeHasProperties<PostResource>(postQueryParameters.Fields))
            {
                return BadRequest("传入的塑形属性不合法");
            }
            #endregion

            //返回带有元数据的PaginatedList数据
            var postsWithMateData = await postRepository.GetAllPosts(postQueryParameters);
            //将posts集合映射为PostResource集合，取出其中的post数据集合
            var postResources = mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(postsWithMateData);
            //针对ResourceModel进行属性塑形
            var shapedPostResources = postResources.ToDynamicIEnumerable(postQueryParameters.Fields);

            #region 针对每个postResource添加HATEOAS的LINKS属性
            var shapedWithLinks = shapedPostResources.Select(x =>
            {
                var dict = x as IDictionary<string, object>;
                var postLinks = CreateLinksForPost((int)dict["id"], postQueryParameters.Fields);
                dict.Add("links", postLinks);
                return dict;
            });
            #endregion

            #region 针对整个postsResource添加HATEOAS的Links属性
            var links = CreateLinksForPosts(postQueryParameters, postsWithMateData.HasPrevious, postsWithMateData.HasNext);

            var result = new
            {
                values = shapedWithLinks,
                links
            };
            #endregion


            #region 提取元数据，并将该数据添加到响应Response自定义X-Pagination的Header中
            //提取元数据
            var mate = new
            {
                postsWithMateData.PageSize,
                postsWithMateData.PageIndex,
                postsWithMateData.TotalItemsCount,
                postsWithMateData.PageCount
            };


            //将翻页元数据添加到响应Header的X-Pagination中
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(mate, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver() //将所有mate元数据属性首字母小写返回
            }));
            #endregion

            //logger.LogError("测试的错误日志记录");
            //throw new Exception("发生了错误");
            return Ok(result);


        }

        [HttpGet(Name = "getPosts")]
        [RequestHeaderMatchingMediaType("Accept", new[] { "application/json","/" })]
        public async Task<IActionResult> Get(PostQueryParameters postQueryParameters)
        {
            #region 参数合法性判断
            //排序字段合法性判断
            if (!string.IsNullOrWhiteSpace(postQueryParameters.OrderBy) &&
                !propertyMappingContainer.ValidateMappingExistsFor<PostResource, Post>(postQueryParameters.OrderBy))
            {
                return BadRequest("排序属性映射关系不存在，或不可通过该排序属性排序");
            }
            //塑形字段合法性判断
            if (!string.IsNullOrWhiteSpace(postQueryParameters.Fields) &&
                !typeHelperService.TypeHasProperties<PostResource>(postQueryParameters.Fields))
            {
                return BadRequest("传入的塑形属性不合法");
            }
            #endregion

            //返回带有元数据的PaginatedList数据
            var postsWithMateData = await postRepository.GetAllPosts(postQueryParameters);
            //将posts集合映射为PostResource集合，取出其中的post数据集合
            var postResources = mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(postsWithMateData);
            //针对ResourceModel进行属性塑形
            var shapedPostResources = postResources.ToDynamicIEnumerable(postQueryParameters.Fields);

            var previousPageLink = postsWithMateData.HasPrevious ? CreatePostUri(postQueryParameters, PaginationResourceUriType.PreviousPage, "getPosts") : null;
            var nextPageLink = postsWithMateData.HasNext ? CreatePostUri(postQueryParameters, PaginationResourceUriType.NextPage, "getPosts") : null;
            #region 提取元数据，并将该数据添加到响应Response自定义X-Pagination的Header中
            //提取元数据
            var mate = new
            {
                postsWithMateData.PageSize,
                postsWithMateData.PageIndex,
                postsWithMateData.TotalItemsCount,
                postsWithMateData.PageCount,
                previousPageLink,
                nextPageLink
            };


            //将翻页元数据添加到响应Header的X-Pagination中
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(mate, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver() //将所有mate元数据属性首字母小写返回
            }));
            #endregion

            //logger.LogError("测试的错误日志记录");
            //throw new Exception("发生了错误");
            return Ok(shapedPostResources);

        }
        #endregion

        #region 使用[FromHeader(Name = "Accept")] string mediaType 参数，在代码中用if判定返回类型的GET方法
        [HttpGet(Name = "getPosts")]
        public async Task<IActionResult> Get_Old(PostQueryParameters postQueryParameters,
            [FromHeader(Name = "Accept")] string mediaType)
        {
            #region 参数合法性判断
            //排序字段合法性判断
            if (!string.IsNullOrWhiteSpace(postQueryParameters.OrderBy) &&
                !propertyMappingContainer.ValidateMappingExistsFor<PostResource, Post>(postQueryParameters.OrderBy))
            {
                return BadRequest("排序属性映射关系不存在，或不可通过该排序属性排序");
            }
            //塑形字段合法性判断
            if (!string.IsNullOrWhiteSpace(postQueryParameters.Fields) &&
                !typeHelperService.TypeHasProperties<PostResource>(postQueryParameters.Fields))
            {
                return BadRequest("传入的塑形属性不合法");
            }
            #endregion

            //返回带有元数据的PaginatedList数据
            var postsWithMateData = await postRepository.GetAllPosts(postQueryParameters);
            //将posts集合映射为PostResource集合，取出其中的post数据集合
            var postResources = mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(postsWithMateData);
            //针对ResourceModel进行属性塑形
            var shapedPostResources = postResources.ToDynamicIEnumerable(postQueryParameters.Fields);
            if (mediaType == "application/vnd.smallprogram.hateoas+json")
            {


                #region 针对每个postResource添加HATEOAS的LINKS属性
                var shapedWithLinks = shapedPostResources.Select(x =>
                {
                    var dict = x as IDictionary<string, object>;
                    var postLinks = CreateLinksForPost((int)dict["id"], postQueryParameters.Fields);
                    dict.Add("links", postLinks);
                    return dict;
                });
                #endregion

                #region 针对整个postsResource添加HATEOAS的Links属性
                var links = CreateLinksForPosts(postQueryParameters, postsWithMateData.HasPrevious, postsWithMateData.HasNext);

                var result = new
                {
                    values = shapedWithLinks,
                    links
                };
                #endregion


                #region 提取元数据，并将该数据添加到响应Response自定义X-Pagination的Header中
                //提取元数据
                var mate = new
                {
                    postsWithMateData.PageSize,
                    postsWithMateData.PageIndex,
                    postsWithMateData.TotalItemsCount,
                    postsWithMateData.PageCount
                };


                //将翻页元数据添加到响应Header的X-Pagination中
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(mate, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver() //将所有mate元数据属性首字母小写返回
                }));
                #endregion

                //logger.LogError("测试的错误日志记录");
                //throw new Exception("发生了错误");
                return Ok(result);
            }
            else
            {
                var previousPageLink = postsWithMateData.HasPrevious ? CreatePostUri(postQueryParameters, PaginationResourceUriType.PreviousPage, "getPosts") : null;
                var nextPageLink = postsWithMateData.HasNext ? CreatePostUri(postQueryParameters, PaginationResourceUriType.NextPage, "getPosts") : null;
                #region 提取元数据，并将该数据添加到响应Response自定义X-Pagination的Header中
                //提取元数据
                var mate = new
                {
                    postsWithMateData.PageSize,
                    postsWithMateData.PageIndex,
                    postsWithMateData.TotalItemsCount,
                    postsWithMateData.PageCount,
                    previousPageLink,
                    nextPageLink
                };


                //将翻页元数据添加到响应Header的X-Pagination中
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(mate, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver() //将所有mate元数据属性首字母小写返回
                }));
                #endregion

                //logger.LogError("测试的错误日志记录");
                //throw new Exception("发生了错误");
                return Ok(shapedPostResources);
            }
        }
        #endregion 


        [HttpGet("{id}", Name = "getPost")]
        public async Task<IActionResult> Get(int id, string fields = null)
        {
            #region 参数合法性判断
            //判断塑形参数合法性
            if (!string.IsNullOrWhiteSpace(fields) &&
                !typeHelperService.TypeHasProperties<PostResource>(fields))
            {
                return BadRequest("塑形属性不合法");
            }
            #endregion

            //获取当个post的Entity
            var post = await postRepository.GetPostById(id);

            //处理没找到的情况返回404
            if (post == null)
            {
                return NotFound();
            }
            //将单个postEntity转换为PostResource
            var postResource = mapper.Map<Post, PostResource>(post);

            //单条数据塑形
            var shapedPostResource = postResource.ToDynamic(fields);

            #region 针对当个post资源添加HATEOAS属性
            var result = shapedPostResource as IDictionary<string, object>;

            var links = CreateLinksForPost(id, fields);

            result.Add("links", links);
            #endregion

            return Ok(result);
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

        #endregion



        #region 辅助方法
        /// <summary>
        /// 生成前一页，后一页，当前页的URI链接方法
        /// </summary>
        /// <param name="parameters">页面的元数据</param>
        /// <param name="uriType">URI类型</param>
        /// <param name="actionName">需要调用的ActionName</param>
        /// <returns></returns>
        private string CreatePostUri(PostQueryParameters parameters, PaginationResourceUriType uriType, string actionName)
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
                    return urlHelper.Link(actionName, previousParameters);
                case PaginationResourceUriType.NextPage:
                    var nextParameters = new
                    {
                        pageIndex = parameters.PageIndex + 1,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields
                    };
                    return urlHelper.Link(actionName, nextParameters);
                default:
                    var currentParameters = new
                    {
                        pageIndex = parameters.PageIndex,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields
                    };
                    return urlHelper.Link(actionName, currentParameters);
            }
        }
        /// <summary>
        /// 为单个资源添加HATEOAS的LINKS
        /// </summary>
        /// <param name="id">资源id</param>
        /// <param name="fields">塑形字段</param>
        /// <returns>LinkResource集合</returns>
        private IEnumerable<LinkResource> CreateLinksForPost(int id, string fields = null)
        {
            var links = new List<LinkResource>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(
                    new LinkResource(
                        urlHelper.Link("GetPost", new { id }), "self", "GET"));
            }
            else
            {
                links.Add(
                    new LinkResource(
                        urlHelper.Link("GetPost", new { id, fields }), "self", "GET"));
            }

            links.Add(
                new LinkResource(
                    urlHelper.Link("DeletePost", new { id }), "delete_post", "DELETE"));

            return links;
        }

        /// <summary>
        /// 为集合资源添加HATEOAS的LINKS
        /// </summary>
        /// <param name="postResourceParameters"></param>
        /// <param name="hasPrevious">是否具有前一页属性</param>
        /// <param name="hasNext">是否具有后一页属性</param>
        /// <returns>LinkResource集合</returns>
        private IEnumerable<LinkResource> CreateLinksForPosts(PostQueryParameters postResourceParameters,
            bool hasPrevious, bool hasNext)
        {
            var links = new List<LinkResource>
            {
                new LinkResource(
                    CreatePostUri(postResourceParameters, PaginationResourceUriType.CurrentPage,"getPosts"),
                    "self", "GET")
            };

            if (hasPrevious)
            {
                links.Add(
                    new LinkResource(
                        CreatePostUri(postResourceParameters, PaginationResourceUriType.PreviousPage, "getPosts"),
                        "previous_page", "GET"));
            }

            if (hasNext)
            {
                links.Add(
                    new LinkResource(
                        CreatePostUri(postResourceParameters, PaginationResourceUriType.NextPage, "getPosts"),
                        "next_page", "GET"));
            }

            return links;
        }

        #endregion
    }
}