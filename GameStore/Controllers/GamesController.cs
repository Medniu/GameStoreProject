using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using GameStore.Interfaces;
using GameStore.Models;
using GameStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

            var topCategoriesViewModel = _mapper.Map<IEnumerable<TopCategoriesDTO>, IEnumerable<TopCategoriesViewModel>>(result);

            return new JsonResult(topCategoriesViewModel);
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

            var gamesInfoViewModel = _mapper.Map<IEnumerable<GamesInfoDTO>, IEnumerable<GameInfoViewModel>>(result);

            if (gamesInfoViewModel == null)
            {
                return StatusCode(404);
            }
            else
            {
                return new JsonResult(gamesInfoViewModel);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> FindGame(int id)
        {
            var result = await _gamesService.FindGameById(id);
            var gamesInfoViewModel = _mapper.Map<GamesInfoDTO, GameInfoViewModel>(result);

            if (gamesInfoViewModel == null)
            {
                return StatusCode(404);
            }
            else
            {
                return new JsonResult(gamesInfoViewModel);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] CreateGameModel createGameModel)
        {
            if (ModelState.IsValid)
            {
                var newGame = _mapper.Map<CreateGameModel, CreateGameModelDTO>(createGameModel);             

                var result = await _gamesService.CreateGame(newGame);

                var gamesInfoViewModel = _mapper.Map<GamesInfoDTO, GameInfoViewModel>(result);

                return new JsonResult(gamesInfoViewModel);               
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
                return StatusCode(204);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit([FromForm] EditGameModel gameModel)
        {
            if (ModelState.IsValid)
            {
                var newGameInfo = _mapper.Map<EditGameModel, EditGameModelDTO>(gameModel);

                var result = await _gamesService.EditGame(gameModel.Id, newGameInfo);

                var gamesInfoViewModel = _mapper.Map<GamesInfoDTO, GameInfoViewModel>(result);

                return new JsonResult(gamesInfoViewModel);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("rating")]
        [Authorize]
        public async Task<IActionResult> Rate([FromBody] GameRatingModel gameRating)
        {
            if (ModelState.IsValid)
            {
                string userId = _userHelper.GetUserId();

                var ratingDTO = _mapper.Map<GameRatingModel, GameRatingDTO>(gameRating);

                ratingDTO.UserId = userId;

                var result = await _gamesService.RateTheGame(ratingDTO);

                return new JsonResult(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
