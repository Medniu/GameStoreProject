using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.ViewModels
{
    public class JwtViewModel
    {
        public Guid UserId { get; set; }
        public bool Result { get; set; }
        public string JwtToken { get; set; }
    }
}
