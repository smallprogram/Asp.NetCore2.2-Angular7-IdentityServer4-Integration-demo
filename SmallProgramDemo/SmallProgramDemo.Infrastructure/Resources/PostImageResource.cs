using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Infrastructure.Resources
{
    public class PostImageResource
    {
        public int id { get; set; }
        public string FileName { get; set; }
        public string Location => $"/uploads/{FileName}";
    }
}
