using DAL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<CategoryInformation>> GetTopCategoriesAsync();
        Task<IEnumerable<GamesInformation>> FindByName(SearchQuery searchQuery);
        Task<Product> FindById(int id);
        Task<Product> Create(GamesInformation gamesInformation);
        Task<bool> Delete(int id);

        Task<Product> Edit(int id, GamesInformation gamesInformation);

    }
}
