using SmallProgramDemo.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Core.Entities
{
    public class Post : Entity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public DateTime LastModified { get; set; }

    }
}
