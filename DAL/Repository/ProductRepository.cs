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
                .Where(s=> s.IsDeleted != true)
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

            var pageInfo = GetInfoOfCurrentPage(sortedList, filtrInformation);

            return pageInfo.Result;
        }

        public async Task<PageInformation> GetInfoOfCurrentPage(IQueryable<GamesInformation> listOfGames,   SortAndFiltrInformation filtrInformation)
        {
            PageInformation pageInformation = new PageInformation
            {
                CurrentPage = (int)filtrInformation.Page,
                PageSize = (int)filtrInformation.PageSize,
                TotalItems = listOfGames.CountAsync().Result,                
            };

            pageInformation.GamesInformation = await listOfGames
                .Skip((int)(filtrInformation.PageSize*(filtrInformation.Page - 1)))
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
                        listOfGames = listOfGames.OrderBy(u => u.TotalRating);
                        break;
                    case "desc":
                        listOfGames = listOfGames.OrderByDescending(u => u.TotalRating);
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
            product.DateCreated = gamesInformation.DateCreated;
            return product;
        }     
    }
}
