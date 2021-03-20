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
            var categoryTable = await context.Products                  
                    .Where(w =>w.IsDeleted != true)
                    .GroupBy(g => g.Category)
                    .OrderByDescending(g => g.Count())
                    .Select(s => new CategoryInformation { Categories = s.Key.ToString(), AmountOfGames = s.Count() })
                    .Take(3)
                    .ToListAsync();                                                           
            return categoryTable;
        }
        public async Task<IEnumerable<GamesInformation>> FindByName (SearchQuery searchQuery)
        {                  
            var listOfGames = await context.Products               
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
                .Take(searchQuery.Limit)
                .ToListAsync();

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
            newProduct.DateCreated = DateTime.UtcNow;

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
                UpdateGameProperties(product, gamesInformation);
                context.Products.Update(product);
                await context.SaveChangesAsync();
                return product;
            }
            else
            {
                return null;
            }
        }

        public async Task<decimal?> Rate(GameRating gameRating)
        {
            var product = await context.ProductRatings
                .Where(p => p.ProductId == gameRating.ProductId)
                .Where(u => u.UserId.ToString() == gameRating.UserId)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                var newProduct = _mapper.Map<GameRating, ProductRating>(gameRating);
                await context.ProductRatings.AddAsync(newProduct);
                await context.SaveChangesAsync();               
            }
            else
            {
                product.Rating = gameRating.Rating;
                context.ProductRatings.Update(product);
                await context.SaveChangesAsync();
            }

            Product updatedGame = await context.Products
                .Where(w => w.IsDeleted != true)
                .FirstOrDefaultAsync(p => p.Id == gameRating.ProductId);

            return updatedGame.TotalRating;            
        }

        public async Task<PageInformation> SortAndFiltr(SortAndFiltrInformation  filtrInformation)
        {

            var listOfGames = context.Products
                .Where(s => s.IsDeleted != true)
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
                }).ToList() ;

            var sortedList = await SortAndFiltrGame(listOfGames, filtrInformation);

            var pageInfo = await GetInfoOfCurrentPage(sortedList, filtrInformation);
        
            return pageInfo;
        }

        public async Task<PageInformation> GetInfoOfCurrentPage(List<GamesInformation> listOfGames,   SortAndFiltrInformation filtrInformation)
        {
            PageInformation pageInformation = new PageInformation
            {
                CurrentPage = (int)filtrInformation.Page,
                PageSize = (int)filtrInformation.PageSize,
                TotalItems = listOfGames.Count(),                
            };

            pageInformation.GamesInformation = listOfGames
                .Skip((int)(filtrInformation.PageSize * (filtrInformation.Page - 1)))
                .Take((int)filtrInformation.PageSize)              
                .ToList();

            return pageInformation;
        }
        public async Task<List<GamesInformation>> SortAndFiltrGame(List<GamesInformation> listOfGames, SortAndFiltrInformation filtrInformation)
        {
            if (listOfGames != null && filtrInformation != null)
            {
                listOfGames = await FiltrGames(listOfGames, filtrInformation);
                listOfGames = await SortGames(listOfGames, filtrInformation);
            }
            return listOfGames;
        }
        public async Task<List<GamesInformation>> FiltrGames (List<GamesInformation> listOfGames, SortAndFiltrInformation filtrInformation)
        {        
            if (listOfGames != null && filtrInformation.FiltrByGenres != null) 
            {
                Categories categories = (Categories)Enum.Parse(typeof(Categories), filtrInformation.FiltrByGenres, true);
                listOfGames = await listOfGames.AsQueryable().Where(p => p.Category == categories).ToListAsync();
            }

            if (listOfGames != null && filtrInformation.FiltrByAge != null)
            {
                Rating rating = (Rating)Enum.Parse(typeof(Rating), filtrInformation.FiltrByAge.ToString(), true);
                listOfGames = await listOfGames.AsQueryable().Where(p => p.Rating == rating).ToListAsync();
            }

            return listOfGames;
        }
        public async Task<List<GamesInformation>> SortGames(List<GamesInformation> listOfGames, SortAndFiltrInformation filtrInformation)
        {
            if (listOfGames != null && filtrInformation.SortByPrice != null)
            {
                switch (filtrInformation.SortByPrice.ToLower())
                {
                    case "asc":
                        listOfGames = await listOfGames.AsQueryable().OrderBy(u => u.Price).ToListAsync();
                        break;
                    case "desc":
                        listOfGames = await listOfGames.AsQueryable().OrderByDescending(u => u.Price).ToListAsync();
                        break;                   
                }                                               
            }

            if (listOfGames != null && filtrInformation.SortByRating != null)
            {
                switch (filtrInformation.SortByRating.ToLower())
                {
                    case "asc":
                        listOfGames = await listOfGames.AsQueryable().OrderBy(u => u.TotalRating).ToListAsync();
                        break;
                    case "desc":
                        listOfGames = await listOfGames.AsQueryable().OrderByDescending(u => u.TotalRating).ToListAsync();
                        break;
                }
            }

            return listOfGames;
        }
        public Product UpdateGameProperties(Product product, GamesInformation gamesInformation)
        {
            product.Logo = gamesInformation.Logo;
            product.Name = gamesInformation.Name;
            product.Price = gamesInformation.Price;                
            product.Background = gamesInformation.Background;
            product.Category = gamesInformation.Category;
            product.Count = gamesInformation.Count; 
            
            return product;
        }     
    }
}
