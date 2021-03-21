﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class SortAndFiltrDTO
    {
        public string? FiltrByGenres { get; set; }
        public int? FiltrByAge { get; set; }
        public string? SortByRating { get; set; }
        public string? SortByPrice { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; } 
    }
}
