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
                    .AsNoTracking()                 
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
                .AsNoTracking()
                .Where(w => w.Name.Contains(searchQuery.Term) && w.IsDeleted != true)                
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
                .AsNoTracking()
                .Where(w => w.IsDeleted != true)
                .FirstOrDefaultAsync(p => p.Id == id);
            
            return gameInfo;
        }

        public async Task<Product> Create(GamesInformation gamesInformation)
        {
            var newProduct = _mapper.Map<GamesInformation, Product>(gamesInformation);
            newProduct.DateCreated = DateTime.UtcNow;
            newProduct.IsDeleted = false;

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
            var productRating = await context.ProductRatings
                .Where(p => p.ProductId == gameRating.ProductId && p.UserId.ToString() == gameRating.UserId)               
                .FirstOrDefaultAsync();

            if (productRating == null)
            {
                var newProduct = _mapper.Map<GameRating, ProductRating>(gameRating);
                newProduct.DateTimeCreated = DateTime.UtcNow;

                await context.ProductRatings.AddAsync(newProduct);
                await context.SaveChangesAsync();               
            }
            else
            {
                productRating.Rating = gameRating.Rating;
                context.ProductRatings.Update(productRating);
                await context.SaveChangesAsync();
            }

            Product updatedGame = await context.Products
                .AsNoTracking()
                .Where(w => w.IsDeleted != true)
                .FirstOrDefaultAsync(p => p.Id == gameRating.ProductId);

            return updatedGame.TotalRating;            
        }

        public async Task<PageInformation> SortAndFiltr(SortAndFiltrInformation  filtrInformation)
        {
            var listOfGames = context.Products
                .AsNoTracking()
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
                });

            var sortedList = SortAndFiltrGame(listOfGames, filtrInformation);

            var pageInfo = await GetInfoOfCurrentPage(sortedList, filtrInformation);
        
            return pageInfo;
        }

        public async Task<PageInformation> GetInfoOfCurrentPage(IQueryable<GamesInformation> listOfGames,   SortAndFiltrInformation filtrInformation)
        {
            PageInformation pageInformation = new PageInformation
            {
                CurrentPage = (int)filtrInformation.Page,
                PageSize = (int)filtrInformation.PageSize,
                TotalItems = listOfGames.Count(),                
            };

            pageInformation.GamesInformation = await listOfGames
                .Skip((int)(filtrInformation.PageSize * (filtrInformation.Page - 1)))
                .Take((int)filtrInformation.PageSize)              
                .ToListAsync();

            return pageInformation;
        }

        public IQueryable<GamesInformation> SortAndFiltrGame(IQueryable<GamesInformation> listOfGames, SortAndFiltrInformation filtrInformation)
        {
            if (listOfGames != null && filtrInformation != null)
            {
                listOfGames = FiltrGames(listOfGames, filtrInformation);
                listOfGames = SortGames(listOfGames, filtrInformation);
            }
            return listOfGames;
        }

        public IQueryable<GamesInformation> FiltrGames (IQueryable<GamesInformation> listOfGames, SortAndFiltrInformation filtrInformation)
        {        
            if (listOfGames != null && filtrInformation.FiltrByGenres != null) 
            {
                Categories categories = (Categories)Enum.Parse(typeof(Categories), filtrInformation.FiltrByGenres, true);
                listOfGames = listOfGames.Where(p => p.Category == categories);
            }

            if (listOfGames != null && filtrInformation.FiltrByAge != null)
            {
                Rating rating = (Rating)Enum.Parse(typeof(Rating), filtrInformation.FiltrByAge.ToString(), true);
                listOfGames = listOfGames.Where(p => p.Rating == rating);
            }

            return listOfGames;
        }

        public IQueryable<GamesInformation> SortGames(IQueryable<GamesInformation> listOfGames, SortAndFiltrInformation filtrInformation)
        {
            if (listOfGames != null && filtrInformation.SortByPrice != null)
            {
                switch (filtrInformation.SortByPrice.ToLower())
                {
                    case "asc":
                        listOfGames = listOfGames.OrderBy(u => u.Price);
                        break;
                    case "desc":
                        listOfGames = listOfGames.OrderByDescending(u => u.Price);
                        break;                   
                }                                               
            }

            if (listOfGames != null && filtrInformation.SortByRating != null)
            {
                switch (filtrInformation.SortByRating.ToLower())
                {
                    case "asc":
                        listOfGames = listOfGames.AsQueryable().OrderBy(u => u.TotalRating);
                        break;
                    case "desc":
                        listOfGames = listOfGames.AsQueryable().OrderByDescending(u => u.TotalRating);
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

        public async Task<Product> AddPicturesUrlToProduct(int Id, string logoUrl, string backgroundUrl)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == Id);

            if(product != null)
            {
                product.Logo = logoUrl;
                product.Background = backgroundUrl;
            }

            context.Products.Update(product);
            await context.SaveChangesAsync();
            return product;
        }
    }
}
