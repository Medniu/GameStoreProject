using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTO
{
    public class OrderDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
    }
}
