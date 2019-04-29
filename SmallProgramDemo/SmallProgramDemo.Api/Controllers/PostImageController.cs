using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmallProgramDemo.Core.Entities;
using SmallProgramDemo.Core.Interface;
using SmallProgramDemo.Infrastructure.Resources;

namespace SmallProgramDemo.Api.Controllers
{
    [Route("api/postimages")]
    public class PostImageController : Controller
    {
        private readonly IPostImageRepository postImageRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHostingEnvironment hostingEnvironment;

        public PostImageController(
            IPostImageRepository postImageRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHostingEnvironment hostingEnvironment)
        {
            this.postImageRepository = postImageRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("文件为空");
            }

            if (file.Length == 0)
            {
                return BadRequest("文件大小为0");
            }

            if (file.Length > 10 * 1024 * 1024)
            {
                return BadRequest("文件不能超过10M");
            }

            var acceptTypes = new[] { ".jpg", ".jpeg", ".png" };
            if (acceptTypes.All(t => t != Path.GetExtension(file.FileName).ToLower()))
            {
                return BadRequest("文件格式不支持，只支持.jpg .jpeg .png格式的文件");
            }

            if (string.IsNullOrWhiteSpace(hostingEnvironment.WebRootPath))
            {
                hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            var uploadsFolderPath = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var postImage = new PostImage
            {
                FileName = fileName
            };

            postImageRepository.Add(postImage);

            await unitOfWork.SaveAsync();

            var result = mapper.Map<PostImage, PostImageResource>(postImage);

            return Ok(result);
        }
    }
}