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
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IS3Service _s3Service;

        public GamesService(IProductRepository repository,  IMapper mapper, IS3Service s3Service)
        {
            _productRepository = repository;
            _mapper = mapper;
            _s3Service = s3Service;
        }

        public async Task<IEnumerable<TopCategoriesDTO>> GetTopCategories()
        {           
            var result = await _productRepository.GetTopCategoriesAsync();

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

            var result = await _productRepository.FindByName(searchInfo);

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
          
            var result = await _productRepository.FindById(id);

            var gameInfo = _mapper.Map<Product, GamesInfoDTO>(result);
            
            return gameInfo;
        }

        public async Task<GamesInfoDTO> CreateGame(CreateGameModelDTO gamesInfoDto)
        {                                 
            var newGame = _mapper.Map<CreateGameModelDTO, GamesInformation>(gamesInfoDto);

            var result = await _productRepository.Create(newGame);
              
            if(result != null)
            {
                var logoPictureUrl = _s3Service.UploadPictureToAws(gamesInfoDto.Logo).Result;
                var backgroundPictureUrl = _s3Service.UploadPictureToAws(gamesInfoDto.Logo).Result;
                
                var updatedModel = await _productRepository.AddPicturesUrlToProduct(result.Id, logoPictureUrl.PictureUrl, backgroundPictureUrl.PictureUrl);
                var fullFieldGameInfo = _mapper.Map<Product, GamesInfoDTO>(updatedModel);
                return fullFieldGameInfo;
            }

            var createdGameWithoutPicture = _mapper.Map<Product, GamesInfoDTO>(result);
            return createdGameWithoutPicture;                  
        }

        public async Task<bool> DeleteGameById(int id)
        {          
            var result = await _productRepository.Delete(id);           
            return result;
        }

        public async Task<GamesInfoDTO> EditGame(int id, EditGameModelDTO gamesInfoDto)
        {                      
            var newGameInfo = _mapper.Map<EditGameModelDTO, GamesInformation>(gamesInfoDto);

            var result = await _productRepository.Edit(gamesInfoDto.Id, newGameInfo);

            if (result != null)
            {
                var logoPictureUrl = _s3Service.UploadPictureToAws(gamesInfoDto.Logo).Result;
                var backgroundPictureUrl = _s3Service.UploadPictureToAws(gamesInfoDto.Logo).Result;

                if (!String.IsNullOrEmpty(logoPictureUrl.PictureUrl) && !String.IsNullOrEmpty(backgroundPictureUrl.PictureUrl))
                {
                    var updatedModel = await _productRepository.AddPicturesUrlToProduct(result.Id, logoPictureUrl.PictureUrl, backgroundPictureUrl.PictureUrl);
                    var updateGameInfo = _mapper.Map<Product, GamesInfoDTO>(updatedModel);
                    return updateGameInfo;
                }
            }

            var editedGameWithOldPictures = _mapper.Map<Product, GamesInfoDTO>(result);           
            return editedGameWithOldPictures;          
        }

        public async Task<decimal?> RateTheGame(GameRatingDTO gameRatingDTO)
        {
            var newGameInfo = _mapper.Map<GameRatingDTO,GameRating >(gameRatingDTO);

            var result = await _productRepository.Rate(newGameInfo);

            return result;
        }

        public async Task<PageDTO> SortAndFiltrGame(SortAndFiltrDTO filtrDTO)
        {
            var searchDto = _mapper.Map<SortAndFiltrDTO, SortAndFiltrInformation>(filtrDTO);

            var result = await _productRepository.SortAndFiltr(searchDto);

            var pageInfo = _mapper.Map<PageInformation, PageDTO>(result);
              
            return pageInfo;
        }

    }
}
