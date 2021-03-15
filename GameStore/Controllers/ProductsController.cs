using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using GameStore.Models;
using GameStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IGamesService _gamesService;
        private readonly IMapper _mapper;        
        public ProductsController(IGamesService gamesService, IMapper mapper )
        {
            _gamesService = gamesService;
            _mapper = mapper;
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchGames([FromQuery] SortAndFiltrModel sortAndFiltrModel)
        {
            var searchDto = _mapper.Map<SortAndFiltrModel, SortAndFiltrDTO>(sortAndFiltrModel);

            var result = await _gamesService.SortAndFiltrGame(searchDto);

            var pageViewModel = _mapper.Map<PageDTO, PageViewModel>(result);

            return new JsonResult(pageViewModel);
        }
    }
}
