using SmallProgramDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Core.Interface
{
    public interface IPostImageRepository
    {
        void Add(PostImage postImage);
    }
}
