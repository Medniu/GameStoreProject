using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
              
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasData(
                new Product
                {
                    Id = 1,
                    Name = "Left 4 Dead",
                    Category = Categories.Shooter,
                    DateCreated = new DateTime(1996, 6, 22),                    
                    Count = 100,
                    Price = 29.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Left4DeadLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Left4DeadBack.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 2,
                    Name = "Battlefield",
                    Category = Categories.Shooter,
                    DateCreated = new DateTime(2018, 9, 4),                  
                    Count = 200,
                    Price = 19.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/BtfLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/BtfBack.png",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 3,
                    Name = "Call of Duty",
                    Category = Categories.Shooter,
                    DateCreated = new DateTime(2015, 7, 15),                 
                    Count = 150,
                    Price = 39.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/CoDLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/CodBack.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 4,
                    Name = "Fifa 21",
                    Category = Categories.Sport,
                    DateCreated = new DateTime(2020, 9, 1),                   
                    Count = 100,
                    Price = 9.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/FifaLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/FifaBack.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 5,
                    Name = "Pes 21",
                    Category = Categories.Sport,
                    DateCreated = new DateTime(2020, 7, 17),                  
                    Count = 50,
                    Price = 29.99m,
                    Rating = Rating.SixPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/PesLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/PesBack.jpeg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 6,
                    Name = "Rocket League",
                    Category = Categories.Sport,
                    DateCreated = new DateTime(2015, 6, 28),                    
                    Count = 100,
                    Price = 19.99m,
                    Rating = Rating.TwelvePlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/RocketLeagLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/RocketLeagback.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 7,
                    Name = "NBA 2k21",
                    Category = Categories.Sport,
                    DateCreated = new DateTime(2020, 3, 9),                   
                    Count = 100,
                    Price = 24.99m,
                    Rating = Rating.TwelvePlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/NbaLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Nbaback.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 8,
                    Name = "Drift 5",
                    Category = Categories.Sport,
                    DateCreated = new DateTime(2017, 06, 13),                    
                    Count = 100,
                    Price = 23.99m,
                    Rating = Rating.TwelvePlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/DriftLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/DriftBack.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 9,
                    Name = "Skyrim",
                    Category = Categories.RPG,
                    DateCreated = new DateTime(2005, 10, 22),                 
                    Count = 1000,
                    Price = 19.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/SkyrimLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/SkyrimBack.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 10,
                    Name = "Fallout 3",
                    Category = Categories.RPG,
                    DateCreated = new DateTime(2017, 8, 12),                  
                    Count = 100,
                    Price = 29.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/FallLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Fallback.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 11,
                    Name = "The Witcher 3",
                    Category = Categories.RPG,
                    DateCreated = new DateTime(2015, 12, 22),                  
                    Count = 100,
                    Price = 49.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/TheWitcherLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/TheWitcherback.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 12,
                    Name = "Cyberpunk 2077",
                    Category = Categories.RPG,
                    DateCreated = new DateTime(2020, 9, 1),                   
                    Count = 100,
                    Price = 19.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/CyberLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/CyberBack.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 13,
                    Name = "Mount and Blade",
                    Category = Categories.RPG,
                    DateCreated = new DateTime(2012, 4, 13),                    
                    Count = 300,
                    Price = 39.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/MountLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Mountback.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 14,
                    Name = "Dark Souls 3",
                    Category = Categories.RPG,
                    DateCreated = new DateTime(2015, 2, 28),                   
                    Count = 200,
                    Price = 19.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/DarkLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/DarkBack.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 15,
                    Name = "Overlord",
                    Category = Categories.RPG,
                    DateCreated = new DateTime(2007, 7, 3),                   
                    Count = 100,
                    Price = 9.99m,
                    Rating = Rating.SixPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/OverLordLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/OverLordBack.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 16,
                    Name = "WarCraft 3",
                    Category = Categories.Strategy,
                    DateCreated = new DateTime(2005, 12, 22),                 
                    Count = 30,
                    Price = 19.99m,
                    Rating = Rating.TwelvePlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/WarLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/WarBack.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 17,
                    Name = "StarCraft 2",
                    Category = Categories.Strategy,
                    DateCreated = new DateTime(2003, 8, 14),                 
                    Count = 100,
                    Price = 29.99m,
                    Rating = Rating.TwelvePlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/StarLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Starback.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 18,
                    Name = "Stronghold Crusader",
                    Category = Categories.Strategy,
                    DateCreated = new DateTime(2002, 9, 22),                  
                    Count = 100,
                    Price = 29.99m,
                    Rating = Rating.SixPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/StrongLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/StrongBack.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 19,
                    Name = "Heroes of Might and Magic 5",
                    Category = Categories.Strategy,
                    DateCreated = new DateTime(2007, 10, 25),                   
                    Count = 100,
                    Price = 29.99m,
                    Rating = Rating.TwelvePlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/HeroesLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Heroesback.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 20,
                    Name = "Syberia",
                    Category = Categories.Quest,
                    DateCreated = new DateTime(2000, 10, 10),                    
                    Count = 100,
                    Price = 4.99m,
                    Rating = Rating.SixPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/SyberiaLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/SyberiaBack.jpg",
                    IsDeleted = false
                },
                new Product
                {
                    Id = 21,
                    Name = "Sherlock Holmes",
                    Category = Categories.Quest,
                    DateCreated = new DateTime(2012, 9, 20),                  
                    Count = 100,
                    Price = 29.99m,
                    Rating = Rating.SixPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/SherlockLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Sherlockback.jpg",
                    IsDeleted = false
                });
               
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .Property(u => u.TotalRating)
                .HasComputedColumnSql("dbo.GetValue(Id)");

           
        }
    }
}
