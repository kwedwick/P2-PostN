using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PostN.WebApi.Models
{
    public class UpdatedUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

     
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string AboutMe { get; set; }
    }
}
