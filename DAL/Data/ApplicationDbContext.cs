﻿using DAL.Entities;
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
                    TotalRating = 9.0m,
                    Count = 100,
                    Price = 29.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Left4DeadLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Left4DeadBack.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "Battlefield",
                    Category = Categories.Shooter,
                    DateCreated = new DateTime(2018, 9, 4),
                    TotalRating = 7.3m,
                    Count = 200,
                    Price = 19.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/BtfLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/BtfBack.png"
                },
                new Product
                {
                    Id = 3,
                    Name = "Call of Duty",
                    Category = Categories.Shooter,
                    DateCreated = new DateTime(2015, 7, 15),
                    TotalRating = 8.1m,
                    Count = 150,
                    Price = 39.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/CoDLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/CodBack.jpg"
                },
                new Product
                {
                    Id = 4,
                    Name = "Fifa 21",
                    Category = Categories.Sport,
                    DateCreated = new DateTime(2020, 9, 1),
                    TotalRating = 9.5m,
                    Count = 100,
                    Price = 9.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/FifaLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/FifaBack.jpg"
                },
                new Product
                {
                    Id = 5,
                    Name = "Pes 21",
                    Category = Categories.Sport,
                    DateCreated = new DateTime(2020, 7, 17),
                    TotalRating = 9.2m,
                    Count = 50,
                    Price = 29.99m,
                    Rating = Rating.SixPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/PesLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/PesBack.jpeg"
                },
                new Product
                {
                    Id = 6,
                    Name = "Rocket League",
                    Category = Categories.Sport,
                    DateCreated = new DateTime(2015, 6, 28),
                    TotalRating = 8.2m,
                    Count = 100,
                    Price = 19.99m,
                    Rating = Rating.TwelvePlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/RocketLeagLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/RocketLeagback.jpg"
                },
                new Product
                {
                    Id = 7,
                    Name = "NBA 2k21",
                    Category = Categories.Sport,
                    DateCreated = new DateTime(2020, 3, 9),
                    TotalRating = 7.2m,
                    Count = 100,
                    Price = 24.99m,
                    Rating = Rating.TwelvePlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/NbaLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Nbaback.jpg"
                },
                new Product
                {
                    Id = 8,
                    Name = "Drift 5",
                    Category = Categories.Sport,
                    DateCreated = new DateTime(2017, 06, 13),
                    TotalRating = 8.2m,
                    Count = 100,
                    Price = 23.99m,
                    Rating = Rating.TwelvePlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/DriftLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/DriftBack.jpg"
                },
                new Product
                {
                    Id = 9,
                    Name = "Skyrim",
                    Category = Categories.RPG,
                    DateCreated = new DateTime(2005, 10, 22),
                    TotalRating = 9.5m,
                    Count = 1000,
                    Price = 19.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/SkyrimLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/SkyrimBack.jpg"
                },
                new Product
                {
                    Id = 10,
                    Name = "Fallout 3",
                    Category = Categories.RPG,
                    DateCreated = new DateTime(2017, 8, 12),
                    TotalRating = 9.5m,
                    Count = 100,
                    Price = 29.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/FallLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Fallback.jpg"
                },
                new Product
                {
                    Id = 11,
                    Name = "The Witcher 3",
                    Category = Categories.RPG,
                    DateCreated = new DateTime(2015, 12, 22),
                    TotalRating = 9.9m,
                    Count = 100,
                    Price = 49.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/TheWitcherLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/TheWitcherback.jpg"
                },
                new Product
                {
                    Id = 12,
                    Name = "Cyberpunk 2077",
                    Category = Categories.RPG,
                    DateCreated = new DateTime(2020, 9, 1),
                    TotalRating = 7.5m,
                    Count = 100,
                    Price = 19.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/CyberLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/CyberBack.jpg"
                },
                new Product
                {
                    Id = 13,
                    Name = "Mount and Blade",
                    Category = Categories.RPG,
                    DateCreated = new DateTime(2012, 4, 13),
                    TotalRating = 9.0m,
                    Count = 300,
                    Price = 39.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/MountLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Mountback.jpg"
                },
                new Product
                {
                    Id = 14,
                    Name = "Dark Souls 3",
                    Category = Categories.RPG,
                    DateCreated = new DateTime(2015, 2, 28),
                    TotalRating = 9.5m,
                    Count = 200,
                    Price = 19.99m,
                    Rating = Rating.EighteenPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/DarkLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/DarkBack.jpg"
                },
                new Product
                {
                    Id = 15,
                    Name = "Overlord",
                    Category = Categories.RPG,
                    DateCreated = new DateTime(2007, 7, 3),
                    TotalRating = 9.1m,
                    Count = 100,
                    Price = 9.99m,
                    Rating = Rating.SixPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/OverLordLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/OverLordBack.jpg"
                },
                new Product
                {
                    Id = 16,
                    Name = "WarCraft 3",
                    Category = Categories.Strategy,
                    DateCreated = new DateTime(2005, 12, 22),
                    TotalRating = 9.7m,
                    Count = 30,
                    Price = 19.99m,
                    Rating = Rating.TwelvePlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/WarLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/WarBack.jpg"
                },
                new Product
                {
                    Id = 17,
                    Name = "StarCraft 2",
                    Category = Categories.Strategy,
                    DateCreated = new DateTime(2003, 8, 14),
                    TotalRating = 9.5m,
                    Count = 100,
                    Price = 29.99m,
                    Rating = Rating.TwelvePlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/StarLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Starback.jpg"
                },
                new Product
                {
                    Id = 18,
                    Name = "Stronghold Crusader",
                    Category = Categories.Strategy,
                    DateCreated = new DateTime(2002, 9, 22),
                    TotalRating = 9.7m,
                    Count = 100,
                    Price = 29.99m,
                    Rating = Rating.SixPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/StrongLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/StrongBack.jpg"
                },
                new Product
                {
                    Id = 19,
                    Name = "Heroes of Might and Magic 5",
                    Category = Categories.Strategy,
                    DateCreated = new DateTime(2007, 10, 25),
                    TotalRating = 9.9m,
                    Count = 100,
                    Price = 29.99m,
                    Rating = Rating.TwelvePlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/HeroesLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Heroesback.jpg"
                },
                new Product
                {
                    Id = 20,
                    Name = "Syberia",
                    Category = Categories.Quest,
                    DateCreated = new DateTime(2000, 10, 10),
                    TotalRating = 6.3m,
                    Count = 100,
                    Price = 4.99m,
                    Rating = Rating.SixPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/SyberiaLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/SyberiaBack.jpg"
                },
                new Product
                {
                    Id = 21,
                    Name = "Sherlock Holmes",
                    Category = Categories.Quest,
                    DateCreated = new DateTime(2012, 9, 20),
                    TotalRating = 9.9m,
                    Count = 100,
                    Price = 29.99m,
                    Rating = Rating.SixPlus,
                    Logo = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/SherlockLogo.jpg",
                    Background = "https://daniilstorepicturebucket.s3.us-east-2.amazonaws.com/Sherlockback.jpg"
                });
               
            base.OnModelCreating(builder);
        }
    }
}
