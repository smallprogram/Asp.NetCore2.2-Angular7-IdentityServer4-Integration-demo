using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public PostController(IPostRepository postRepository,IUnitOfWork unitOfWork)
        {
            this.postRepository = postRepository;
            this.unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await postRepository.GetAllPosts();
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