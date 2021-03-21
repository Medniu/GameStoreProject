using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DTO
{
    public class PageInformation
    {
        public List<GamesInformation> GamesInformation { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
    }
}
