using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class PageDTO
    {
        public ICollection<GamesInformation> GamesInformation { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
    }
}
