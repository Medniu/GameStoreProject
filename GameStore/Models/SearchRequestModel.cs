using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class SearchRequestModel
    {   
        [Required]        
        public string Term { get; set; }
    
        public int? Limit { get; set; }
    }
}
