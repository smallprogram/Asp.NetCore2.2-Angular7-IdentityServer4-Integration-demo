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
                    myContext.Posts.AddRange(
                        new List<Post>{
                            new Post{
                                Title = "Post Title 1",
                                Body = "Post Body 1",
                                Author = "zhusir",
                                LastModified = DateTime.Now
                            },
                            new Post{
                                Title = "Post Title 2",
                                Body = "Post Body 2",
                                Author = "zhusir",
                                LastModified = DateTime.Now
                            },
                            new Post{
                                Title = "Post Title 3",
                                Body = "Post Body 3",
                                Author = "zhusir",
                                LastModified = DateTime.Now
                            },
                            new Post{
                                Title = "Post Title 4",
                                Body = "Post Body 4",
                                Author = "zhusir",
                                LastModified = DateTime.Now
                            },
                            new Post{
                                Title = "Post Title 5",
                                Body = "Post Body 5",
                                Author = "zhusir",
                                LastModified = DateTime.Now
                            },
                            new Post{
                                Title = "Post Title 6",
                                Body = "Post Body 6",
                                Author = "zhusir",
                                LastModified = DateTime.Now
                            },
                            new Post{
                                Title = "Post Title 7",
                                Body = "Post Body 7",
                                Author = "zhusir",
                                LastModified = DateTime.Now
                            },
                            new Post{
                                Title = "Post Title 8",
                                Body = "Post Body 8",
                                Author = "zhusir",
                                LastModified = DateTime.Now
                            }
                        }
                    );
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
