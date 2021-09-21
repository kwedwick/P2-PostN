using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PostN.Domain
{
    public interface IPostRepo
    {
        Task<List<Post>> GetAllPosts();
        Task<Post> GetPostById(int id);

        Task<Post> CreatePost(Post post);

        Task<Post> UpdatePostById(int id, Post post);

        Task<bool> DeletePostByIdAsync(int id);

        Task<Comment> CreateCommentByPostId(int postId, Comment comment);

        Task<Comment> UpdateCommentById(int commentId, Comment comment);

        Task<bool> DeleteCommentByIdAsync(int postId, int commentId);

        Task<List<FriendPosts>> GetFriendsPosts(int userId);
    }
}
