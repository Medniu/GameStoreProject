using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class ProductRating
    {
        [Key]
        public int Id { get; set; }        
        public int ProductId { get; set; }
        public Guid UserId { get; set; }

        [Column("Rating", TypeName = "decimal(5,1)")]
        public decimal Rating { get; set; }
        public DateTime DateTimeCreated { get; set; } = DateTime.UtcNow;
    }
}
