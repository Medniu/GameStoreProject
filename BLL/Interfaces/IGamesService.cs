using BLL.DTO;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGamesService
    {
        Task<IEnumerable<TopCategoriesDTO>> GetTopCategories();
        Task<IEnumerable<GamesInfoDTO>> FindGameByName(SearchQueryDTO queryDTO);
    }
}
