using System;
using System.Collections.Generic;
using System.Text;
using SmallProgramDemo.Core.Entities;
using SmallProgramDemo.Core.Interface;
using SmallProgramDemo.Infrastructure.Database;

namespace SmallProgramDemo.Infrastructure.Repository
{
    public class PostImageRepository : IPostImageRepository
    {
        private readonly MyContext myContext;

        public PostImageRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public void Add(PostImage postImage)
        {
            myContext.PostImages.Add(postImage);
        }
    }
}
