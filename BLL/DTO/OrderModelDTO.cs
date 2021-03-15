using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class OrderModelDTO
    {        
        public int ProductId { get; set; }        
        public int Quantity { get; set; }
        public string UserId { get; set;  }
    }
}
