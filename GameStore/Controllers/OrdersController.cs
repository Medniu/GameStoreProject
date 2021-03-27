using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using GameStore.Interfaces;
using GameStore.Models;
using GameStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService; 
        private readonly IMapper _mapper;
        private readonly IUserHelper _userHelper;

        public OrdersController(IOrderService orderService, IMapper mapper, IUserHelper userHelper)
        {
            _orderService = orderService;
            _mapper = mapper;
            _userHelper = userHelper;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddProductToOrder([FromBody] OrderModel orderModel)
        {
            if (ModelState.IsValid)
            {
                string userId = _userHelper.GetUserId();

                var orderDto = _mapper.Map<OrderModel, OrderModelDTO>(orderModel);
                orderDto.UserId = userId;

                var result = await _orderService.AddProductToOrder(orderDto);

                if(result == true)
                {
                    return StatusCode(201);
                }
                else
                {
                    return BadRequest("Something go wrong, please try again later");
                }                       
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProductFromCurrentOrder([FromQuery] string orderId)
        {        
            var userId = _userHelper.GetUserId();

            var result = await _orderService.GetProductFromOrder(orderId,userId);
            
            var gamesInfoViewModel = _mapper.Map<IEnumerable<GamesInfoDTO>, IEnumerable<GameInfoViewModel>>(result);

            return new JsonResult(gamesInfoViewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<int> deletedGamesId)
        {
            var userId = _userHelper.GetUserId();

            DeletedGameModelDTO deletedGames = new DeletedGameModelDTO
            {
                UserId = userId,
                deletedGamesID = deletedGamesId
            };

            await _orderService.DeleteProductsFromOrder(deletedGames);

            return StatusCode(204);
        }

        [HttpPost("buy")]
        public async Task<IActionResult> Buy()
        {
            var userId = _userHelper.GetUserId();

            if (await _orderService.Buy(userId))
            {
                return StatusCode(204);
            }
            else
            {
                return StatusCode(404);
            }
        }
    }
}
