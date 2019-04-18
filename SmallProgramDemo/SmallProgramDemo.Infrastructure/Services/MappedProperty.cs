using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Infrastructure.Services
{
    /// <summary>
    /// Resource Model与EntityModel的属性映射对的容器
    /// </summary>
    public class MappedProperty
    {
        //映射的属性名
        public string Name { get; set; }
        //是否为反向映射
        public bool Revert { get; set; }
    }
}
