namespace Forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Humanizer;
    using Forum.Models;
    using Logan.Utilities;

    internal sealed class Configuration : DbMigrationsConfiguration<Forum.Models.ForumEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Forum.Models.ForumEntities context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddOrUpdate(
                    new Role
                    {
                        CreatedBy = "System",
                        CreatedOn = DateTime.UtcNow,
                        Name = "Admin"
                    },
                    new Role
                    {
                        CreatedBy = "System",
                        CreatedOn = DateTime.UtcNow,
                        Name = "Moderator"
                    },
                    new Role
                    {
                        CreatedBy = "System",
                        CreatedOn = DateTime.UtcNow,
                        Name = "User"
                    }
                );
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.AddOrUpdate(
                    new User
                    {
                        Avatar = "/Content/Images/Avatars/ortund.jpg",
                        Banned = false,
                        BanReason = String.Empty,
                        Bio = "Youtuber, gamer, entrepreneur and animal lover",
                        CreatedBy = "System",
                        CreatedOn = DateTime.UtcNow,
                        EmailAddress = "ortund@fudge",
                        FirstName = "Logan",
                        LastName = "Young",
                        Password = Hashing.CreateHash("moomin"),
                        Role = context.Roles.FirstOrDefault(x => x.Id == 1),
                        RoleId = 1,
                        Signature = "Anybody that gets abusive when presented with empirical evidence is driving an agenda you're not aware of.",
                        ResetToken = Guid.NewGuid().ToString(),
                        Username = "Ortund"
                    },
                    new User
                    {
                        Avatar = "/Content/Images/Avatars/iluvatar.png",
                        Banned = false,
                        BanReason = String.Empty,
                        Bio = "WoW Elemental Shaman",
                        CreatedBy = "System",
                        CreatedOn = DateTime.UtcNow,
                        EmailAddress = "ilu@fudge.com",
                        FirstName = "Logan",
                        LastName = "Young",
                        Password = Hashing.CreateHash("moomin"),
                        Role = context.Roles.FirstOrDefault(x => x.Id == 1),
                        RoleId = 3,
                        ResetToken = Guid.NewGuid().ToString(),
                        Username = "Ilüvatar"
                    }
                );
                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddOrUpdate(
                    new Category
                    {
                        Name = "Programming",
                        CreatedBy = "System Seed",
                        CreatedOn = DateTime.UtcNow
                    },
                    new Category
                    {
                        Name = "Gaming",
                        CreatedBy = "System Seed",
                        CreatedOn = DateTime.UtcNow
                    },
                    new Category
                    {
                        Name = "Lifestyle",
                        CreatedBy = "System Seed",
                        CreatedOn = DateTime.UtcNow
                    },
                    new Category
                    {
                        Name = "Pets",
                        CreatedBy = "System Seed",
                        CreatedOn = DateTime.UtcNow
                    }
                );
                context.SaveChanges();
            }

            if (!context.Titles.Any())
            {
                context.Titles.AddOrUpdate(
                    new Title { Category = context.Categories.FirstOrDefault(x => x.Id == 1), Name = "C# Development", Description = "For C# Discussion and Q & A only.", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow },
                    new Title { Category = context.Categories.FirstOrDefault(x => x.Id == 1), Name = "VB Development", Description = "Visual Basic discussion and questions.", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow },
                    new Title { Category = context.Categories.FirstOrDefault(x => x.Id == 1), Name = "Other Development", Description = "Software development discussion and questions that don't fit other forums.", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow },
                    new Title { Category = context.Categories.FirstOrDefault(x => x.Id == 2), Name = "World of Warcraft", Description = "Anything that has anything to do with WoW.", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow },
                    new Title { Category = context.Categories.FirstOrDefault(x => x.Id == 2), Name = "PC Gaming", Description = "Discuss games, tips, cheats and also ask your pc hardware questions here.", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow },
                    new Title { Category = context.Categories.FirstOrDefault(x => x.Id == 2), Name = "Other Gaming", Description = "Can't find a dedicated forum for your topic? Here's the melting pot at your service.", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow },
                    new Title { Category = context.Categories.FirstOrDefault(x => x.Id == 3), Name = "Physical Fitness", Description = "Post about your fitness queries or show off your progress.", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow }
                );
                context.SaveChanges();
            }

            if (!context.Threads.Any())
            {
                context.Threads.AddOrUpdate(
                    new Thread
                    {
                        CreatedBy = "System",
                        CreatedOn = DateTime.UtcNow,
                        Name = "This is an example thread to demonstrate how data will appear on the forums",
                        Title = context.Titles.FirstOrDefault(x => x.Id == 1),
                        TitleId = 1,
                        UserId = 1
                    }
                );
                context.SaveChanges();
            }

            if (!context.Posts.Any())
            {
                context.Posts.AddOrUpdate(
                    new Post
                    {
                        Content = "Posts on Fudge use Markdown to format the text so as long as Markdown can do it, you can add **bold text**, *italic text* and basically whatever type of content you want to your posts.",
                        CreatedBy = "System",
                        CreatedOn = DateTime.UtcNow,
                        Thread = context.Threads.FirstOrDefault(x => x.Id == 1),
                        ThreadId = 1,
                        User = context.Users.FirstOrDefault(x => x.Id == 1),
                        UserId = 1,
                        DateString = DateTime.UtcNow.Humanize()
                    },
                    new Post
                    {
                        Content = "This post is just here to pad the content and demonstrate what the page will look like with multiple posts.",
                        CreatedBy = "System",
                        CreatedOn = DateTime.UtcNow,
                        Thread = context.Threads.FirstOrDefault(x => x.Id == 1),
                        ThreadId = 1,
                        User = context.Users.FirstOrDefault(x => x.Id == 1),
                        UserId = 1,
                        DateString = DateTime.UtcNow.Humanize()
                    }
                );

                // add Opinions to simulate actions on posts
                context.Opinions.Add(new Opinion { Action = Models.PostAction.Like, PostId = 1, UserId = 1 });
                context.Opinions.Add(new Opinion { Action = Models.PostAction.Dislike, PostId = 2, UserId = 1 });

                context.SaveChanges();
            }
        }
    }
}
