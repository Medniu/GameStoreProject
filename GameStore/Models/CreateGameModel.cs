using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class CreateGameModel
    {
        [Required]        
        public string Name { get; set; }

        [Required]    
        public Categories Category { get; set; }

        [Required]   
        public Rating Rating { get; set; }

        [Required]       
        public string Logo { get; set; }

        [Required]     
        public string Background { get; set; }

        [Required]    
        public decimal Price { get; set; }

        [Required]   
        public int Count { get; set; }
        [Required]     
        public DateTime DateCreated { get; set; }
        [Required]        
        public decimal TotalRating { get; set; }
    }
}
