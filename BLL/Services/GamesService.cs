using AutoMapper;
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
        private readonly IS3Service _s3Service;
        public GamesService(IProductRepository repository,  IMapper mapper, IS3Service s3Service)
        {
            _repository = repository;
            _mapper = mapper;
            _s3Service = s3Service;
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
       
            var listOfGames = result.Select(s => new GamesInfoDTO
            {
                Name = s.Name,
                Category = s.Category.ToString(),
                TotalRating = s.TotalRating,
                DateCreated = s.DateCreated,
                Logo = s.Logo,
                Price = s.Price,
                Background = s.Background,
                Rating = s.Rating.ToString(),
                Count = s.Count
            });

            return listOfGames;
        }
        public async Task<GamesInfoDTO> FindGameById(int id)
        {
          
            var result = await _repository.FindById(id);

            var gameInfo = _mapper.Map<Product, GamesInfoDTO>(result);
            
            return gameInfo;
        }
        public async Task<GamesInfoDTO> CreateGame(CreateGameModelDTO gamesInfo)
        {           
            var response = _s3Service.UploadPictureToAws(gamesInfo).Result;

            if (response.gamesInfoDTO != null)
            {
                var newGame = _mapper.Map<GamesInfoDTO, GamesInformation>(response.gamesInfoDTO);

                var result = await _repository.Create(newGame);

                var newGameInfo = _mapper.Map<Product, GamesInfoDTO>(result);

                return newGameInfo;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteGameById(int id)
        {          
            var result = await _repository.Delete(id);           
            return result;
        }

        public async Task<GamesInfoDTO> EditGame(int id, EditGameModelDTO gamesInfo)
        {
            var response = _s3Service.UpdatePictureOnAws(gamesInfo).Result;

            if (response != null)
            {
                var newGameInfo = _mapper.Map<GamesInfoDTO, GamesInformation>(response.gamesInfoDTO);

                var result = await _repository.Edit(gamesInfo.Id, newGameInfo);

                var updateGameInfo = _mapper.Map<Product, GamesInfoDTO>(result);

                return updateGameInfo;
            }

            else 
            {
                return null;
            }
        }

        public async Task<decimal?> RateTheGame(GameRatingDTO gameRatingDTO)
        {
            var newGameInfo = _mapper.Map<GameRatingDTO,GameRating >(gameRatingDTO);

            var result = await _repository.Rate(newGameInfo);

            return result;
        }

        public async Task<PageDTO> SortAndFiltrGame(SortAndFiltrDTO filtrDTO)
        {
            var searchDto = _mapper.Map<SortAndFiltrDTO, SortAndFiltrInformation>(filtrDTO);

            var result = await _repository.SortAndFiltr(searchDto);

            var pageInfo = _mapper.Map<PageInformation, PageDTO>(result);
              
            return pageInfo;
        }

    }
}
