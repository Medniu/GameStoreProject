using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.DTO;
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
                Category = s.Category,
                TotalRating = s.TotalRating,
                DateCreated = s.DateCreated
            });

            return listOfGames;
        }
       
    }
}
