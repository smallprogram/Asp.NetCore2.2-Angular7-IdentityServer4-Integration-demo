using SmallProgramDemo.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Core.Entities
{

    /// <summary>
    /// uri传入的参数封装类
    /// </summary>
    public class PostQueryParameters : QueryParameters
    {
        public string Title { get; set; }
    }
}
