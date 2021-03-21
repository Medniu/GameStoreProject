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
        public DateTime DateCreated { get; set; }
        public DateTime? DateOrdered { get; set; }
        public bool Status { get; set; }     
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
