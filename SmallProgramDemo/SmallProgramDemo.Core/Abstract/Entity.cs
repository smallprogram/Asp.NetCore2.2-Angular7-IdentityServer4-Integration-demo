using SmallProgramDemo.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Core.Abstract
{
    /// <summary>
    /// Entity抽象父类
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// id主键属性
        /// </summary>
        public int id { get; set; }
    }
}
