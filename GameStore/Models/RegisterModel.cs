using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "email", Description = "Email address")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        [EmailAddress]
        public string Email { get; set; }        

        [Required]
        [RegularExpression(@"^(375)?(25|33|44|29)\d{7}$")]        
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,20}$")]
        [Display(Name = "password", Description = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Role { get; set; } = "User";
    }  
}
