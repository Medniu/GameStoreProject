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
using Microsoft.Extensions.Caching.Memory;

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
        private readonly IMemoryCache _cache;

        public UserService( UserManager<User> userManager, RoleManager<Role> roleManager,
                            ApplicationDbContext dbContext, IOptionsSnapshot<JwtSettings> jwtSettings,
                            IEmailService emailService, IMapper mapper, IMemoryCache cache)
        {
            _applicationDbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _emailService = emailService;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<bool> ChangePassword(string userId, UserDTO userDTO )
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {              
                IdentityResult result =
                    await _userManager.ChangePasswordAsync(user, userDTO.Password, userDTO.NewPassword);

                return result.Succeeded;               
            }
            else 
            { 
                return false;
            }                 
        }

        public async Task<ChangedUserDTO> ChangeInfo(string userId, UserDTO userDTO)
        {
            var changedUserDto = new ChangedUserDTO();          
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
                    changedUserDto.Result = true;
                    changedUserDto.UserDTO = userDto;                    
                    return changedUserDto;
                }
                else 
                {
                    changedUserDto.Result = false;
                    changedUserDto.UserDTO = null;
                    return changedUserDto; 
                }
            }
            else 
            {
                changedUserDto.Result = false;
                changedUserDto.UserDTO = null;
                return changedUserDto;
            }
        }

        public async Task<UserDTO> GetInfo (string userId)
        {
            User user = null;

            if (!_cache.TryGetValue(userId, out user))
            {
                user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    _cache.Set(user.Id, user,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }

            var userDto = _mapper.Map<User, UserDTO>(user);
            return userDto;               
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

        public async Task<JwtDTO> Authorize(UserDTO userDTO)
        {
            var jwtDto = new JwtDTO();
          
            var user = _userManager.Users.SingleOrDefault(u => u.Email == userDTO.Email);

            if (user is null)
            {
                jwtDto.Result = false;
                jwtDto.JwtToken = string.Empty;
                return jwtDto;
            }

            var userSigninResult = await _userManager.CheckPasswordAsync(user, userDTO.Password);

            if (userSigninResult)
            {
                var roles = await _userManager.GetRolesAsync(user);
                jwtDto.Result = true;
                jwtDto.JwtToken = GenerateJwt(user,roles);
                return jwtDto;
            }
            else
            {
                jwtDto.Result = false;
                jwtDto.JwtToken = string.Empty;
                return jwtDto;
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
