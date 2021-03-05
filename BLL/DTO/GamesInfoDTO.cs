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
        public decimal TotalRating { get; set; }
    }
}
