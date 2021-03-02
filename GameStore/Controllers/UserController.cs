using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using GameStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _userService.GetInfo(userId);

            var userProfile = _mapper.Map<UserDTO, UserProfile>(result);

            return new JsonResult(userProfile);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeUserInfo([FromBody] UserProfile userProfile)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userDto = _mapper.Map<UserProfile, UserDTO>(userProfile);

            var result = await _userService.ChangeInfo(userId, userDto);

            userProfile = _mapper.Map<UserDTO, UserProfile>(result.Item2);

            if (result.Item1 == true)
            {
                return new JsonResult(userProfile);
            }
            return Problem();
        }


        [HttpPost]
        [Route("password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel passwordModel)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userDto = _mapper.Map<ChangePasswordModel, UserDTO>(passwordModel);

            var result = await _userService.ChangePassword(userId, userDto);
            if (result == true)
            {
                return Ok();
            }
            else return Problem();
        } 
    }
}
