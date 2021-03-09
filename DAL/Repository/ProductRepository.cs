using DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;
using DAL.DTO;
using DAL.Entities;
using AutoMapper;

namespace DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper _mapper;
        public ProductRepository(ApplicationDbContext context, IMapper mapper)            
        {
            this.context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryInformation>> GetTopCategoriesAsync()
        {
            var categoryTable = context.Products
                    .AsEnumerable()
                    .Where(w =>w.IsDeleted != true)
                    .GroupBy(g => g.Category)
                    .OrderByDescending(g => g.Count())
                    .Select(s => new CategoryInformation { Categories = s.Key.ToString(), AmountOfGames = s.Count() })
                    .Take(3)
                    .ToList();                                                           
            return categoryTable;
        }
        public async Task<IEnumerable<GamesInformation>> FindByName (SearchQuery searchQuery)
        {                  
            var listOfGames = context.Products               
                .Where(w => w.Name.Contains(searchQuery.Term))
                .Where(w =>w.IsDeleted !=true)
                .Select(s => new GamesInformation
                {
                    Name = s.Name,
                    Category = s.Category,
                    TotalRating = s.TotalRating,
                    DateCreated = s.DateCreated,
                    Logo = s.Logo,
                    Price = s.Price,
                    Background = s.Background,
                    Rating = s.Rating,
                    Count = s.Count                    
                })
                .Take(searchQuery.Limit);

            return listOfGames;
        }

        public async Task<Product> FindById(int id)
        {
            Product gameInfo = await context.Products
                .Where(w => w.IsDeleted != true)
                .FirstOrDefaultAsync(p => p.Id == id);
            
            return gameInfo;
        }

        public async Task<Product> Create(GamesInformation gamesInformation)
        {
            var newProduct = _mapper.Map<GamesInformation, Product>(gamesInformation);

            var result = await context.Products.AddAsync(newProduct);

            await context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id); 

            if (product != null && product.IsDeleted == false)
            {
                product.IsDeleted = true;               
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Product> Edit(int id, GamesInformation gamesInformation)
        {            
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null && product.IsDeleted != true)
            {
                UpdateProperties(product, gamesInformation);
                context.Products.Update(product);
                await context.SaveChangesAsync();
                return product;
            }
            else
            {
                return null;
            }
        }

        public Product UpdateProperties(Product product, GamesInformation gamesInformation)
        {
            product.Logo = gamesInformation.Logo;
            product.Name = gamesInformation.Name;
            product.Price = gamesInformation.Price;
            product.Rating = gamesInformation.Rating;
            product.TotalRating = gamesInformation.TotalRating;
            product.Background = gamesInformation.Background;
            product.Category = gamesInformation.Category;
            product.Count = gamesInformation.Count;
            product.DateCreated = gamesInformation.DateCreated;
            return product;
        }
    }
}
