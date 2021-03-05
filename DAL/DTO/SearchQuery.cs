using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTO
{
    public class SearchQuery
    {
        public string Term { get; set; }
        public int Limit { get; set; }
    }
}
