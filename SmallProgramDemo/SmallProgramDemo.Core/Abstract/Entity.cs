using SmallProgramDemo.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Core.Abstract
{
    public abstract class Entity : IEntity
    {
        public int id { get; set; }
    }
}
