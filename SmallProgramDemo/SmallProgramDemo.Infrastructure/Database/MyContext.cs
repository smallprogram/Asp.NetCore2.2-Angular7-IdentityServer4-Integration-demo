using Microsoft.EntityFrameworkCore;
using SmallProgramDemo.Core.Entities;
using SmallProgramDemo.Infrastructure.Database.EntityConfiguration;

namespace SmallProgramDemo.Infrastructure.Database
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        //使用实体配置约束
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PostConfiguration());//使用实体配置约束
        }

        public DbSet<Post> Posts { set; get; }
        public DbSet<PostImage> PostImages { set; get; }
    }
}
