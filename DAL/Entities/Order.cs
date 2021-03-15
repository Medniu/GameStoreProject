using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateOrdered { get; set; } = null;
        public bool Status { get; set; } = false;      
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
