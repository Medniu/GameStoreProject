using BLL.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<bool> ChangePassword(string userId, UserDTO userDTO);
        Task<(bool, UserDTO)> ChangeInfo(string userId, UserDTO userDTO);
        Task<UserDTO> GetInfo(string userId);
        Task<bool> Create(UserDTO userDto);
        Task<(bool,string)> Authorize(UserDTO userDTO);       
    }
}
