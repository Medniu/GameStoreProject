

using GameStore.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GameStore.Helper
{   
    public class UserHelper : IUserHelper
    {
        IHttpContextAccessor httpContextAccessor;
        public UserHelper(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            var HttpContext = httpContextAccessor.HttpContext;           
            return HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }              
    } 
}
