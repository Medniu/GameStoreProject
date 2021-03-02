using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class UserProfile
    {
        public string Name { get; set; }

        [Required]
        [Display(Name = "email", Description = "Email address")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(375)?(25|33|44|29)\d{7}$")]
        public string PhoneNumber { get; set; }
    }
}
