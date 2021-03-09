﻿using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.DTO;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GamesService : IGamesService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public GamesService(IProductRepository repository,  IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TopCategoriesDTO>> GetTopCategories()
        {           
            var result = await _repository.GetTopCategoriesAsync();

            var topCategories = result
                .Select(s => new TopCategoriesDTO
                {
                    Categories = s.Categories,
                    AmountOfGames = s.AmountOfGames
                });

            return topCategories;
        }
        public async Task<IEnumerable<GamesInfoDTO>> FindGameByName(SearchQueryDTO searchQuery)
        {           
            var searchInfo = _mapper.Map<SearchQueryDTO, SearchQuery>(searchQuery);
            var result = await _repository.FindByName(searchInfo);
       
            var listOfGames =result.Select(s => new GamesInfoDTO
            {
                Name = s.Name,
                Category = s.Category.ToString(),
                TotalRating = s.TotalRating,
                DateCreated = s.DateCreated,
                Logo = s.Logo,
                Price = s.Price,
                Background = s.Background,
                Rating = s.Rating.ToString()
            });

            return listOfGames;
        }
        public async Task<GamesInfoDTO> FindGameById(int id)
        {
          
            var result = await _repository.FindById(id);

            var gameInfo = _mapper.Map<Product, GamesInfoDTO>(result);
            
            return gameInfo;
        }
        public async Task<GamesInfoDTO> CreateGame(GamesInfoDTO gamesInfo)
        {
            var newGame = _mapper.Map<GamesInfoDTO, GamesInformation>(gamesInfo);

            var result = await _repository.Create(newGame);
            var newGameInfo = _mapper.Map<Product, GamesInfoDTO>(result);
            return newGameInfo;
        }

        public async Task<bool> DeleteGameById(int id)
        {          
            var result = await _repository.Delete(id);           
            return result;
        }

        public async Task<GamesInfoDTO> EditGame(int id, GamesInfoDTO gamesInfo)
        {
            var newGameInfo = _mapper.Map<GamesInfoDTO, GamesInformation>(gamesInfo);

            var result = await _repository.Edit(id,newGameInfo);
             
            var updateGameInfo = _mapper.Map<Product, GamesInfoDTO>(result);

            return updateGameInfo;
        }
    }
}
