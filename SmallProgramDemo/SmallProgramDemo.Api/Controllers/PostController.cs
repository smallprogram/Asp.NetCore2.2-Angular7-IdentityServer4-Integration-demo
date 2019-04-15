using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallProgramDemo.Infrastructure.Database;

namespace SmallProgramDemo.Api.Controllers
{
    [Route("api/posts")]
    public class PostController : Controller
    {
        public PostController(MyContext mycontext)
        {
            Mycontext = mycontext;
        }

        public MyContext Mycontext { get; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await Mycontext.Posts.ToListAsync();
            return Ok(posts);
        }
    }
}