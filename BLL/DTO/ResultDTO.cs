using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class ResultDTO
    {
        public bool Result { get; set; }
        public UserDTO? UserDTO { get; set; }
        public string? JwtToken { get; set; }
    }
}
