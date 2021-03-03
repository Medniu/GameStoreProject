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
        Task<ResultDTO> ChangeInfo(string userId, UserDTO userDTO);
        Task<UserDTO> GetInfo(string userId);
        Task<bool> Create(UserDTO userDto);
        Task<ResultDTO> Authorize(UserDTO userDTO);       
    }
}
