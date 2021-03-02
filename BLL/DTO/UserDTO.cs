using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class UserDTO 
    {       
        public string Name { get; set; }      
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }        
        public string Role { get; set; }
        public string NewPassword { get; set; }
    }
}
