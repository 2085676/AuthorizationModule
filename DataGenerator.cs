using AuthorizationModule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AuthorizationModule
{
    // Generate dummy data
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DBContext(
                serviceProvider.GetRequiredService<DbContextOptions<DBContext>>()))
            {
                if (context.Users.Any())
                {
                    return;
                }

                context.Users.AddRange(
                    new User
                    {
                        Id = 1,
                        Email = "sarthak@gmail.com",
                        Password = "123456"
                    },
                    new User
                    {
                        Id = 2,
                        Email = "nikhil@gmail.com",
                        Password = "123456"
                    },
                    new User
                    {
                        Id = 3,
                        Email = "kapish@gmail.com",
                        Password = "123456"
                    },
                    new User
                    {
                        Id = 4,
                        Email = "aishwarya@gmail.com",
                        Password = "123456"
                    },
                    new User
                    {
                        Id = 5,
                        Email = "nandita@gmail.com",
                        Password = "123456"
                    },
                    new User
                    {
                        Id = 6,
                        Email = "parnavi@gmail.com",
                        Password = "123456"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
