using BLL.DTO;
using BLL.Interfaces;
using BLL.Settings;
using DAL.Data;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ApplicationDbContext _applicationDbContext;        
        private readonly JwtSettings _jwtSettings;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public UserService( UserManager<User> userManager, RoleManager<Role> roleManager,
                            ApplicationDbContext dbContext, IOptionsSnapshot<JwtSettings> jwtSettings,
                            IEmailService emailService, IMapper mapper)
        {
            _applicationDbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _emailService = emailService;
            _mapper = mapper;
        }
        public async Task<bool> ChangePassword(string userId, UserDTO userDTO )
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                IdentityResult result =
                    await _userManager.ChangePasswordAsync(user, userDTO.Password, userDTO.NewPassword);

                if (result.Succeeded)
                {
                    return true;
                }
                else
                { 
                    return false;
                }
            }
            else 
            { 
                return false;
            }                 
        }
        public async Task<ResultDTO> ChangeInfo(string userId, UserDTO userDTO)
        {
            var resultDto = new ResultDTO();
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.UserName = userDTO.Name;
                user.Email = userDTO.Email;
                user.PhoneNumber = userDTO.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var userDto = _mapper.Map<User, UserDTO>(user);
                    resultDto.Result = true;
                    resultDto.UserDTO = userDto;
                    return resultDto;
                }
                else 
                {
                    resultDto.Result = false;
                    resultDto.UserDTO = null;
                    return resultDto; 
                }
            }
            else 
            {
                resultDto.Result = false;
                resultDto.UserDTO = null;
                return resultDto;
            }
        }
        public async Task<UserDTO> GetInfo (string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var userDto = _mapper.Map<User, UserDTO>(user);
                           
            return  userDto;                   
        }
        public async Task<bool> Create(UserDTO userDto)
        {                                 
            var user = _mapper.Map<UserDTO, User>(userDto);

            var userCreateResult = await _userManager.CreateAsync(user, userDto.Password);
                    
            await _userManager.AddToRoleAsync(user, userDto.Role);
            await _userManager.SetPhoneNumberAsync(user, userDto.PhoneNumber);
            await _userManager.UpdateAsync(user);


            if (userCreateResult.Succeeded)
            {
                await _emailService.SendEmailAsync(userDto.Email, "Registration", $"You have successfully registered");
                return true;
            }
            else 
            {
                return false;
            }       
        }      
        public async Task<ResultDTO> Authorize(UserDTO userDTO)
        {
            var resultDto = new ResultDTO();
            var user = _userManager.Users.SingleOrDefault(u => u.Email == userDTO.Email);

            if (user is null)
            {
                resultDto.Result = false;
                resultDto.JwtToken = string.Empty;
                return resultDto;
            }

            var userSigninResult = await _userManager.CheckPasswordAsync(user, userDTO.Password);

            if (userSigninResult)
            {
                var roles = await _userManager.GetRolesAsync(user);
                resultDto.Result = true;
                resultDto.JwtToken = GenerateJwt(user,roles);
                return resultDto;
            }
            else
            {
                resultDto.Result = false;
                resultDto.JwtToken = string.Empty;
                return resultDto;
            }
        }     
        public void Dispose()
        {
            _applicationDbContext.Dispose();
            _roleManager.Dispose();
            _userManager.Dispose();
        }
        public async Task SaveAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
        public string GenerateJwt(User user, IList<string> roles)
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
