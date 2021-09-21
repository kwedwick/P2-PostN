using System;
using System.Collections.Generic;

#nullable disable

namespace PostN.DataAccess.Entities
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            FollowerUserId2Navigations = new HashSet<Follower>();
            FollowerUsers = new HashSet<Follower>();
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AboutMe { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DoB { get; set; }
        public byte[] ProfilePic { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Follower> FollowerUserId2Navigations { get; set; }
        public virtual ICollection<Follower> FollowerUsers { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
