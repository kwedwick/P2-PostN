using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PostN.WebApi.Models
{
    public class CreatedUser
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Please enter more than 5 characters")]
        public string Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Enter at least 6 characters")]
        public string Password { get; set; }
        public string AboutMe { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Please enter date in correct form")]
        public DateTime DoB { get; set; }
    }
}
