using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
   
    [Index(nameof(Name), nameof(Category), nameof(DateCreated), nameof(TotalRating))]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("ProductName", TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Required]
        [Column ("CategoryIndex", TypeName = "int")]
        public Categories Category { get; set; }

        [Required]
        [Column ("AgeRating", TypeName = "int")]
        public Rating Rating { get; set; }
        
        [Required]
        [Column ("Logo", TypeName = "varchar(200)")]
        public string Logo { get; set; }

        [Required]
        [Column("BackGround", TypeName = "varchar(200)")]
        public string Background { get; set; }

        [Required]
        [Column("Price", TypeName ="decimal(5,2)")]
        public decimal Price { get; set; }

        [Required]
        [Column("Count")]
        public int Count { get; set; }

        [Required]
        [Column("DateOfCreation" , TypeName ="date")]
        public DateTime DateCreated { get; set; }

        [Required]
        [Column("TotalRating", TypeName = "decimal(5,1)")]
        public decimal TotalRating { get; set; }

        [Required]
        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }

    public enum Categories
    {
        Shooter,
        Sport,
        RPG,
        Strategy,
        Quest
    }
    public enum Rating
    {
        SixPlus = 6,
        TwelvePlus = 12,
        EighteenPlus = 18
    }
}
