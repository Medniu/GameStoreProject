﻿using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
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
using GameStore.ViewModels;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {     
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public AuthController(IUserService userService, IMapper mapper, ILogService logService)
        {
            _userService = userService;
            _mapper = mapper;
            _logService = logService;
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

                var jwtViewModel = _mapper.Map<JwtDTO, JwtViewModel>(result);

                if (jwtViewModel.Result)
                {
                    _logService.SuccessSignInLogs(jwtViewModel.UserId);

                    return Ok(jwtViewModel.JwtToken);
                }
                else
                {
                    _logService.UnsuccessSignInLogs();

                    return BadRequest();
                }
            }
            else
            {
                return BadRequest(ModelState);
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
                    return BadRequest(ModelState);                  
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }            
    }
}
