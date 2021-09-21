using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostN.Domain
{
    public class Post
    {

        public Post() { }

        public Post(int id, int userId, byte[] image, DateTime created, string title, string body, string username)
        {
            Id = id;
            UserId = userId;
            Image = image;
            Created = created;
            Title = title;
            Body = body;
            Username = username;
        }

        public Post(int id, int userId, string username, byte[] image, DateTime created, string title, string body, List<Comment> comments)
        {
            Id = id;
            UserId = userId;
            Username = username;
            Image = image;
            Created = created;
            Title = title;
            Body = body;
            Comments = comments;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public byte[] Image { get; set; }
        public DateTime Created { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public string Username { get; set; }
        public List<Comment> Comments { get; set; }    
    }
}
