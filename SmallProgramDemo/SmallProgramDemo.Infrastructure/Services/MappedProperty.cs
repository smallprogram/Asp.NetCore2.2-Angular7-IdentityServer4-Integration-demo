using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Infrastructure.Services
{
    /// <summary>
    /// EntityModel的属性映射的容器，用于排序时使用对应的EntityModel的属性映射名称与是否为相反顺序的排序映射
    /// </summary>
    public class MappedProperty
    {

        /// <summary>
        /// 映射的属性名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否为相反顺序的排序映射
        /// </summary>
        public bool Revert { get; set; }
    }
}
