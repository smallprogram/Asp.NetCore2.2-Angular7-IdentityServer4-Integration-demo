using Microsoft.EntityFrameworkCore;
using SmallProgramDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallProgramDemo.Infrastructure.Database
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {

        }
        public DbSet<Post> Posts { set; get; }
    }
}
