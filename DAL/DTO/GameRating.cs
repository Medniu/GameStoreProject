using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTO
{
    public class GameRating
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Rating { get; set; }
    }
}
