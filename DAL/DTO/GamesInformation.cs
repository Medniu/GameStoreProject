using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTO
{
    public class GamesInformation
    {
        public string Name { get; set; }
        public Categories Category { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal TotalRating { get; set; }
        public int Count { get; set; }
        public Rating Rating { get; set; }

        public string Logo { get; set; }    
        public string Background { get; set; }

        public decimal Price { get; set; }
    }
}
