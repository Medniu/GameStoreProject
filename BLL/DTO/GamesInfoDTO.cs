using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class GamesInfoDTO
    {
        public string Name { get; set; }       
        public string Category { get; set; }
        public DateTime DateCreated { get; set; }  
        public decimal? TotalRating { get; set; }
        public string Rating { get; set; }
        public string Logo { get; set; }
        public string Background { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
