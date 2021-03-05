using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTO
{
    public class GamesInformation
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal TotalRating { get; set; }
    }
}
