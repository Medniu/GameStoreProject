using DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;
using DAL.DTO;

namespace DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;
        public ProductRepository(ApplicationDbContext context)            
        {
            this.context = context;
        }

        public async Task<IEnumerable<CategoryInformation>> GetTopCategoriesAsync()
        {
            var categoryTable = context.Products
                    .AsEnumerable()
                    .GroupBy(g => g.Category)
                    .OrderByDescending(g => g.Count())
                    .Select(s => new CategoryInformation { Categories = s.Key.ToString(), AmountOfGames = s.Count() })
                    .Take(3)
                    .ToList();                    ;                                       
            return categoryTable;
        }
        public async Task<IEnumerable<GamesInformation>> FindByName (SearchQuery searchQuery)
        {                  
            var listOfGames = context.Products               
                .Where(w => w.Name.Contains(searchQuery.Term))
                .Select(s => new GamesInformation
                {
                    Name = s.Name,
                    Category = s.Category.ToString(),
                    TotalRating = s.TotalRating,
                    DateCreated = s.DateCreated
                })
                .Take(searchQuery.Limit);
            return listOfGames;
        }
    
    }
}
