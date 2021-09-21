using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostN.WebApi.Models
{
    public class CreatedPost
    {
        public int UserId { get; set; }
        public byte[] Image { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
