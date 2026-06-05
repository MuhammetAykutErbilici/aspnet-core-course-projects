using BlogApp.Web.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Web.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<BlogContext>();

                if (context != null)
                {
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }

                    if (!context.Tags.Any())
                    {
                        context.Tags.AddRange(
                            new Tag { Text = "Web programlama", Url = "web-programlama", Color = TagColors.info },
                            new Tag { Text = "Backend", Url = "backend", Color = TagColors.warning },
                            new Tag { Text = "Frontend", Url = "frontend", Color = TagColors.success },
                            new Tag { Text = "Fullstack", Url = "fullstack", Color = TagColors.secondary },
                            new Tag { Text = "Php", Url = "php", Color = TagColors.primary }
                        );
                        context.SaveChanges();
                    }

                    if (!context.Users.Any())
                    {
                        context.Users.AddRange(
                            new User { UserName = "SadikTuran", Image = "1.vesikalık.webp" },
                            new User { UserName = "AhmetYılmaz", Image = "2.vesikalık.jpg" }
                        );
                        context.SaveChanges();
                    }

                    if (!context.Posts.Any())
                    {
                        // Kullanıcıları kesin olarak değişkenlere atıyoruz
                        var user1 = context.Users.FirstOrDefault(u => u.UserName == "SadikTuran");
                        var user2 = context.Users.FirstOrDefault(u => u.UserName == "AhmetYılmaz");

                        // Tüm nesneleri (Post ve altındaki Comment'leri) tamamen dinamik ID'lere bağlıyoruz
                        context.Posts.AddRange(
                            new Post
                            {
                                Title = "Asp.Net Core",
                                Content = "Asp.Net Core dersleri",
                                Url = "aspnet-core",
                                isAvtive = true,
                                PublishedOn = DateTime.Now.AddDays(-10),
                                Tags = context.Tags.Take(3).ToList(),
                                Image = "1.jpg",
                                UserId = user1.UserId, // Eğitmenin sabit 1'i yerine dinamik yaptık
                                Comments = new List<Comment>
                                {
                new Comment { Text = "İyi bir kurs", PublishedOn = DateTime.Now, UserId = user1.UserId },
                new Comment { Text = "Çok faydalı bir kurs", PublishedOn = DateTime.Now, UserId = user1.UserId }
                                }
                            },
                            new Post
                            {
                                Title = "Php ",
                                Content = "Php dersleri",
                                Url = "php",
                                isAvtive = true,
                                PublishedOn = DateTime.Now.AddDays(-20),
                                Tags = context.Tags.Take(2).ToList(),
                                Image = "2.jpg",
                                UserId = user1.UserId
                            },
                            new Post
                            {
                                Title = "Django ",
                                Content = "Django dersleri",
                                Url = "django",
                                isAvtive = true,
                                PublishedOn = DateTime.Now.AddDays(-30),
                                Tags = context.Tags.Take(4).ToList(),
                                Image = "3.jpg",
                                UserId = user1.UserId
                            },
                            new Post
                            {
                                Title = "React ",
                                Content = "React dersleri",
                                Url = "react",
                                isAvtive = true,
                                PublishedOn = DateTime.Now.AddDays(-40),
                                Tags = context.Tags.Take(4).ToList(),
                                Image = "3.jpg",
                                UserId = user2.UserId
                            },
                            new Post
                            {
                                Title = "Angular ",
                                Content = "Angular dersleri",
                                Url = "angular",
                                isAvtive = true,
                                PublishedOn = DateTime.Now.AddDays(-24),
                                Tags = context.Tags.Take(4).ToList(),
                                Image = "3.jpg",
                                UserId = user2.UserId
                            },
                            new Post
                            {
                                Title = "Web Tasarım ",
                                Content = "Web tasarımlı dersleri",
                                Url = "web-tasarim",
                                isAvtive = true,
                                PublishedOn = DateTime.Now.AddDays(-50),
                                Tags = context.Tags.Take(4).ToList(),
                                Image = "3.jpg",
                                UserId = user2.UserId
                            }
                        );
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}