using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using GameStore.Interfaces;
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
        private readonly IUserHelper _userHelper;
        public GamesController(IGamesService gamesService, IMapper mapper, IUserHelper userHelper)
        {
            _gamesService = gamesService;
            _mapper = mapper;
            _userHelper = userHelper;
        }

        [HttpGet]
        [Route("topCategories")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTopCategories()
        {
            var result = await _gamesService.GetTopCategories();

            return new JsonResult(result);
        }


        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchGames([FromQuery] SearchRequestModel searchRequest)
        {
            if (searchRequest.Limit == null || searchRequest.Limit <= 0)
            {
                searchRequest.Limit = 10;
            }
            var searchDto = _mapper.Map<SearchRequestModel, SearchQueryDTO>(searchRequest);

            var result = await _gamesService.FindGameByName(searchDto);

            return new JsonResult(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> FindGame(int id)
        {
            var result = await _gamesService.FindGameById(id);

            return new JsonResult(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateGameModel gameModel)
        {
            if (ModelState.IsValid)
            {
                var newGame = _mapper.Map<CreateGameModel, GamesInfoDTO>(gameModel);
                var result = await _gamesService.CreateGame(newGame);
                return new JsonResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _gamesService.DeleteGameById(id);
            if (result == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit([FromBody] EditGameModel gameModel)
        {
            if (ModelState.IsValid)
            {
                var newGameInfo = _mapper.Map<EditGameModel, GamesInfoDTO>(gameModel);
                var result = await _gamesService.EditGame(gameModel.Id, newGameInfo);
                return new JsonResult(result);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("rating")]
        [Authorize]
        public async Task<IActionResult> Rate([FromBody] GameRatingModel gameRating)
        {
            string userId = _userHelper.GetUserId();

            var ratingDTO = _mapper.Map<GameRatingModel, GameRatingDTO>(gameRating);

            ratingDTO.UserId = userId;

            var result = await _gamesService.RateTheGame(ratingDTO);

            return new JsonResult(result);
        }
    }
}
