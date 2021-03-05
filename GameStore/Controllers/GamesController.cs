using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using GameStore.Models;
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
    public class GamesController : ControllerBase
    {
        private readonly IGamesService _gamesService;
        private readonly IMapper _mapper;
        public GamesController(IGamesService gamesService, IMapper mapper)
        {
            _gamesService = gamesService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("topCategories")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTopCategories()
        {
            var result =  await _gamesService.GetTopCategories();
           
            return new JsonResult(result);
        }
      
        
        [HttpGet("search")]                       
        [AllowAnonymous]
        public async Task<IActionResult> SearchGames([FromQuery] SearchRequestModel searchRequest)
        {
            if (searchRequest.Limit == null || searchRequest.Limit <=0)
            {
                searchRequest.Limit = 10;
            }
            var searchDto = _mapper.Map<SearchRequestModel, SearchQueryDTO>(searchRequest);

            var result = await _gamesService.FindGameByName(searchDto);

            return new JsonResult(result);
                                    
        }      
    }
}
