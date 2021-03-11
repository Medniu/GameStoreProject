using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class SortAndFiltrModel
    {
        public string? Genres { get; set; }
        public int? Age{ get; set; }        
        public string? Rating { get; set; }
        public string? Price { get; set; }
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 3;

    }
}
