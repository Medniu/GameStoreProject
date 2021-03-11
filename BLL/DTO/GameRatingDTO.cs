using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class GameRatingDTO
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public decimal GameRating { get; set; } 
    }
}
