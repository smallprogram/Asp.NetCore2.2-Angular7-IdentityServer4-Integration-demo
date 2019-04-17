using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmallProgramDemo.Core.Entities;
using SmallProgramDemo.Core.Interface;
using SmallProgramDemo.Infrastructure.Database;
using SmallProgramDemo.Infrastructure.Resources;

namespace SmallProgramDemo.Api.Controllers
{
    [Route("api/posts")]
    public class PostController : Controller
    {
        private readonly IPostRepository postRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<PostController> logger;
        private readonly IMapper mapper;

        public PostController(
            IPostRepository postRepository,
            IUnitOfWork unitOfWork, 
            ILogger<PostController> logger,
            IMapper mapper)
        {
            this.postRepository = postRepository;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await postRepository.GetAllPosts();
            //将posts集合映射为PostResource集合
            var postResource = mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(posts);

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
    }
}