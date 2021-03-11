using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class GameRatingModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public decimal Rating { get; set; }
    }
}
