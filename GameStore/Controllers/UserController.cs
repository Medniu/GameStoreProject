using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using GameStore.Helper;
using GameStore.Interfaces;
using GameStore.Models;
using GameStore.ViewModels;
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
        private readonly IUserHelper _userHelper;
        public UserController(IUserService userService, IMapper mapper, IUserHelper userHelper)
        {
            _userService = userService;
            _mapper = mapper;
            _userHelper = userHelper;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {          
            string userId = _userHelper.GetUserId();

            var result = await _userService.GetInfo(userId);

            var userProfile = _mapper.Map<UserDTO, UserProfileViewModel>(result);

            return new JsonResult(userProfile);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeUserInfo([FromBody] UserProfileModel userProfile)
        {
            if (ModelState.IsValid)
            {                              
                string userId = _userHelper.GetUserId();

                var userDto = _mapper.Map<UserProfileModel, UserDTO>(userProfile);

                var result = await _userService.ChangeInfo(userId, userDto);           

                var updatedUserProfile = _mapper.Map<UserDTO, UserProfileViewModel>(result.UserDTO);

                if (result.Result == true)
                {
                    return new JsonResult(updatedUserProfile);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }            
        }


        [HttpPost]
        [Route("password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel passwordModel)
        {          
            string userId = _userHelper.GetUserId();

            if (ModelState.IsValid)
            {
                var userDto = _mapper.Map<ChangePasswordModel, UserDTO>(passwordModel);

                var result = await _userService.ChangePassword(userId, userDto);
                if (result == true)
                {
                    return StatusCode(204);
                }
                else
                {
                    return BadRequest();
                }
            }            
            else
            {
                return BadRequest(ModelState);
            }            
        }        
    }
}
