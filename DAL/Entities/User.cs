using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class User : IdentityUser<Guid>
    {
        public ICollection<ProductRating> Ratings { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
