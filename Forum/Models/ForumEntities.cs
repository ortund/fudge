using Logan.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class ForumEntities : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
        
        public ForumEntities()
            : base("DefaultConnection")
        {
            //Database.SetInitializer<ForumEntities>(new MigrateDatabaseToLatestVersion<ForumEntities, Migrations.Configuration>());
        }

        public void Seed()
        {
            if (!Categories.Any())
            {
                List<Category> NewCategories = new List<Category>
                {
                    new Category { Name = "Programming", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow },
                    new Category { Name = "Gaming", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow },
                    new Category { Name = "Lifestyle", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow },
                    new Category { Name = "Pets", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow }
                };

                foreach (Category c in NewCategories)
                {
                    Categories.Add(c);
                }
                SaveChanges();
            }
            if (!Titles.Any())
            {
                List<Title> NewTitles = new List<Title>
                {
                new Title { Category = Categories.FirstOrDefault(x => x.Id == 1), Name = "C# Development", Description = "For C# Discussion and Q & A only.", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow },
                new Title { Category = Categories.FirstOrDefault(x => x.Id == 1), Name = "VB Development", Description = "Visual Basic discussion and questions.", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow },
                new Title { Category = Categories.FirstOrDefault(x => x.Id == 1), Name = "Other Development", Description = "Software development discussion and questions that don't fit other forums.", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow },
                new Title { Category = Categories.FirstOrDefault(x => x.Id == 2), Name = "World of Warcraft", Description = "Anything that has anything to do with WoW.", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow },
                new Title { Category = Categories.FirstOrDefault(x => x.Id == 2), Name = "PC Gaming", Description = "Discuss games, tips, cheats and also ask your pc hardware questions here.", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow },
                new Title { Category = Categories.FirstOrDefault(x => x.Id == 2), Name = "Other Gaming", Description = "Can't find a dedicated forum for your topic? Here's the melting pot at your service.", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow },
                new Title { Category = Categories.FirstOrDefault(x => x.Id == 3), Name = "Physical Fitness", Description = "Post about your fitness queries or show off your progress.", CreatedBy = "System Seed", CreatedOn = DateTime.UtcNow }
                };

                foreach (Title t in NewTitles)
                {
                    Titles.Add(t);
                }
                SaveChanges();
            }
            if (!Roles.Any())
            {
                List<Role> NewRoles = new List<Role>
                {
                    new Role
                {
                    Name = "Admin",
                    CreatedBy = "System Seed",
                    CreatedOn = DateTime.UtcNow
                },
                new Role
                {
                    Name = "Moderator",
                    CreatedBy = "System Seed",
                    CreatedOn = DateTime.UtcNow
                },
                new Role
                {
                    Name = "User",
                    CreatedBy = "System Seed",
                    CreatedOn = DateTime.UtcNow
                }
                };

                foreach (Role r in NewRoles)
                {
                    Roles.Add(r);
                }
                SaveChanges();
            }
            if (!Users.Any())
            {
                User User = new User
                {
                    Avatar = "~/Content/Images/Avatars/ortund.jpg",
                    Banned = false,
                    BanReason = String.Empty,
                    Bio = String.Empty,
                    CreatedBy = "System Data Seed",
                    CreatedOn = DateTime.UtcNow,
                    Deleted = false,
                    EmailAddress = "logan.young@vodamail.co.za",
                    FirstName = "Logan",
                    LastName = "Young",
                    ModifiedBy = String.Empty,
                    ModifiedOn = null,
                    Password = Hashing.CreateHash("Ly@224593"),
                    Role = Roles.FirstOrDefault(x => x.Id == 1),
                    RoleId = 1,
                    Signature = "Anybody that gets abusive when presented with empirical evidence is driving an agenda you're not aware of.",
                    ResetToken = Guid.NewGuid().ToString(),
                    Username = "Ortund"
                };

                Users.Add(User);
                SaveChanges();
            }
        }
    }
}