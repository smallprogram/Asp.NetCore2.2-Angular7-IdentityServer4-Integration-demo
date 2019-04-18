using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using SmallProgramDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmallProgramDemo.Infrastructure.Database
{
    //数据库数据初始化Seed，在Api项目Program中调用
    public class MyContextSeed
    {
        public static async Task SeedAsync(MyContext myContext,
                          ILoggerFactory loggerFactory, int retry = 0)
        {
            int retryForAvailability = retry;
            try
            {
                // TODO: Only run this if using a real database
                // myContext.Database.Migrate();

                if (!myContext.Posts.Any())
                {

                    #region 无序数据添加
                    //myContext.Posts.AddRange(
                    //    new List<Post>{
                    //        new Post{
                    //            Title = "Post Title 1",
                    //            Body = "Post Body 1",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 2",
                    //            Body = "Post Body 2",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 3",
                    //            Body = "Post Body 3",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 4",
                    //            Body = "Post Body 4",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 5",
                    //            Body = "Post Body 5",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 6",
                    //            Body = "Post Body 6",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 7",
                    //            Body = "Post Body 7",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 8",
                    //            Body = "Post Body 8",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 9",
                    //            Body = "Post Body 9",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 10",
                    //            Body = "Post Body 10",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 11",
                    //            Body = "Post Body 11",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 12",
                    //            Body = "Post Body 12",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 13",
                    //            Body = "Post Body 13",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 14",
                    //            Body = "Post Body 14",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 15",
                    //            Body = "Post Body 15",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 16",
                    //            Body = "Post Body 16",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 17",
                    //            Body = "Post Body 17",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 18",
                    //            Body = "Post Body 18",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 19",
                    //            Body = "Post Body 19",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 20",
                    //            Body = "Post Body 20",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 21",
                    //            Body = "Post Body 21",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 22",
                    //            Body = "Post Body 22",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 23",
                    //            Body = "Post Body 23",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 24",
                    //            Body = "Post Body 24",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 25",
                    //            Body = "Post Body 25",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 26",
                    //            Body = "Post Body 26",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 27",
                    //            Body = "Post Body 27",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 28",
                    //            Body = "Post Body 28",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 29",
                    //            Body = "Post Body 29",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 30",
                    //            Body = "Post Body 30",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 31",
                    //            Body = "Post Body 31",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        },
                    //        new Post{
                    //            Title = "Post Title 32",
                    //            Body = "Post Body 32",
                    //            Author = "zhusir",
                    //            LastModified = DateTime.Now
                    //        }
                    //    }
                    //);
                    #endregion

                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 1",
                        Body = "Post Body 1",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 2",
                        Body = "Post Body 44",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 3",
                        Body = "Post Body 3",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 4",
                        Body = "Post Body 233",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 5",
                        Body = "Post Body 5",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 6",
                        Body = "Post Body 70",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 7",
                        Body = "Post Body 7",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 8",
                        Body = "Post Body 8",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 9",
                        Body = "Post Body 9",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 10",
                        Body = "Post Body 50",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 11",
                        Body = "Post Body 11",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 12",
                        Body = "Post Body 12",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 13",
                        Body = "Post Body 13",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 14",
                        Body = "Post Body 44",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 15",
                        Body = "Post Body 15",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 80",
                        Body = "Post Body 16",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 17",
                        Body = "Post Body 17",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 18",
                        Body = "Post Body 18",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 88",
                        Body = "Post Body 19",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 20",
                        Body = "Post Body 20",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 21",
                        Body = "Post Body 21",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 22",
                        Body = "Post Body 22",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 23",
                        Body = "Post Body 23",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 24",
                        Body = "Post Body 24",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 25",
                        Body = "Post Body 25",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 26",
                        Body = "Post Body 26",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 27",
                        Body = "Post Body 27",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 28",
                        Body = "Post Body 28",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 29",
                        Body = "Post Body 29",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 30",
                        Body = "Post Body 30",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 31",
                        Body = "Post Body 31",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });await myContext.SaveChangesAsync();
                    myContext.Posts.Add(new Post
                    {
                        Title = "Post Title 32",
                        Body = "Post Body 32",
                        Author = "zhusir",
                        LastModified = DateTime.Now
                    });

                    await myContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var logger = loggerFactory.CreateLogger<MyContextSeed>();
                    logger.LogError(ex.Message);
                    await SeedAsync(myContext, loggerFactory, retryForAvailability);
                }
            }
        }
    }
}
