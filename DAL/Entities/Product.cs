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
        [Column("DateOfCreation" , TypeName ="date")]
        public DateTime DateCreated { get; set; }

        [Required]
        [Column("TotalRating",TypeName = "decimal")]
        public decimal TotalRating { get; set; }
    }

    public enum Categories
    {
        Shooter,
        Sport,
        RPG,
        Strategy,
        Quest
    }
}
