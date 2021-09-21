using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostN.WebApi.Models
{
    public class CreatedComment
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int PostId { get; set; }
        [Required]
        public string CommentBody { get; set; }
    }
}
