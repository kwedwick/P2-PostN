using System;

namespace PostN.Domain
{
    public class Comment
    {
        public Comment() { }
        public Comment(int userId, string username, int postId, DateTime created, string commentBody)
        {
            UserId = userId;
            Username = username;
            PostId = postId;
            Created = created;
            CommentBody = commentBody;
        }

        public Comment(int id, int userId, int postId, DateTime created, string commentBody)
        {
            Id = id;
            UserId = userId;
            PostId = postId;
            Created = created;
            CommentBody = commentBody;
        }

        public Comment(int id, int userId, string username, int postId, DateTime created, string commentBody)
        {
            Id = id;
            UserId = userId;
            Username = username;
            PostId = postId;
            Created = created;
            CommentBody = commentBody;
        }

        public Comment(int id, int userId, int postId, string username, DateTime created, string commentBody)
        {
            Id = id;
            UserId = userId;
            Username = username;
            PostId = postId;
            Created = created;
            CommentBody = commentBody;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int PostId { get; set; }
        public DateTime Created { get; set; }
        public string CommentBody { get; set; }
    }
}