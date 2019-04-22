using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Infrastructure.Resources
{
    /// <summary>
    /// 用于添加或修改POST的Resource类
    /// </summary>
    public class PostAddOrUpdateResource
    {
        public string Title { get; set; }
        public string Body { get; set; }

        public string Remark { get; set; }
    }
}
