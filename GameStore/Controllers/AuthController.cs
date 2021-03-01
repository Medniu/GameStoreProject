using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using DAL.Data;
using System.Web;
using GameStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using AutoMapper;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {     
        private IUserService _userService;
        private readonly IMapper _mapper;
        public AuthController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;          
        }       

        [HttpPost]
        [Route("signIn")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] LoginModel loginModel)
        {

            if (ModelState.IsValid)
            {
                var userDto = _mapper.Map<LoginModel, UserDTO>(loginModel);
              
                var result = await _userService.Authorize(userDto);

                if (result.Item1)
                {
                    return Ok(result.Item2);
                }
                else
                {
                    return Problem("401");
                }
            }
            else
            {
                return Problem("401");
            }

        }

        [HttpPost]
        [Route("signUp")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody]RegisterModel registerModel)
        {

            if (ModelState.IsValid)
            {
                var userDto = _mapper.Map<RegisterModel, UserDTO>(registerModel);             

                var result = await _userService.Create(userDto);

                if (result)
                {                
                    return Ok();
                }
                else
                {
                    return Problem("400");                  
                }
            }

            else
            {
                return Problem("400");
            }
        }            
    }
}
