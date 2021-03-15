using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.DTO;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        

        public async Task<bool> AddProductToOrder(OrderModelDTO orderDTO)
        {
            var orderDto = _mapper.Map<OrderModelDTO, OrderDTO>(orderDTO);

            var result = await _repository.AddToOrder(orderDto);

            return result;
        }

        
        public async Task<IEnumerable<GamesInfoDTO>> GetProductFromOrder(string orderId)
        {
            var result = await _repository.GetProductFromOrder(orderId);       
            
            var productList = result.Select(s => new GamesInfoDTO
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

            return productList;
        }        
        public async Task DeleteProductsFromOrder(DeletedGameModelDTO deletedGamesId)
        {
            var deletedGames = _mapper.Map<DeletedGameModelDTO, DeletedGameDTO>(deletedGamesId);

            await _repository.Delete(deletedGames);
        }
        public async Task Buy(string userId)
        {
            await _repository.CompleteOrder(userId);
        }
    }
}
