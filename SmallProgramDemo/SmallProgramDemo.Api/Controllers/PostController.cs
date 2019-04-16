using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmallProgramDemo.Core.Entities;
using SmallProgramDemo.Core.Interface;
using SmallProgramDemo.Infrastructure.Database;

namespace SmallProgramDemo.Api.Controllers
{
    [Route("api/posts")]
    public class PostController : Controller
    {
        private readonly IPostRepository postRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<PostController> logger;

        public PostController(
            IPostRepository postRepository,
            IUnitOfWork unitOfWork, 
            ILogger<PostController> logger)
        {
            this.postRepository = postRepository;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await postRepository.GetAllPosts();
            //logger.LogError("测试的错误日志记录");
            //throw new Exception("发生了错误");
            return Ok(posts);
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