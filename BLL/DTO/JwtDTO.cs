using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class JwtDTO
    {
        public Guid UserId { get; set; }
        public bool Result { get; set; }        
        public string JwtToken { get; set; }
    }
}
