using Microsoft.EntityFrameworkCore;
using PostN.DataAccess.Entities;
using PostN.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostN.DataAccess
{
    public class PostRepo : IPostRepo
    {
        private readonly CMKWDTP2Context _context;
        public PostRepo(CMKWDTP2Context context)
        {
            _context = context;
        }


        /*public List<Domain.Post> GetPost()
        {
            return _context.Posts.Select(
                users => new Domain.Post(users.Id, users.UserId, users.Image, users.Created, users.Title, users.Body)
            ).ToList();
        }*/

        public async Task<Domain.Comment> CreateCommentByPostId(int postId, Domain.Comment comment)
        {

            var newEntity = new Entities.Comment
            {
                UserId = comment.UserId,
                PostId = comment.PostId,
                Created = comment.Created,
                CommentBody = comment.CommentBody
            };
            await _context.Comments.AddAsync(newEntity);
            _context.SaveChanges();
            comment.Id = newEntity.Id;
            return comment;
        }

        public async Task<Domain.Post> CreatePost(Domain.Post post)
        {
            var newEntity = new Entities.Post
            {
                UserId = post.UserId,
                Image = post.Image,
                Created = post.Created,
                Title = post.Title,
                Body = post.Body,
            };
            await _context.Posts.AddAsync(newEntity);
            await _context.SaveChangesAsync();
            post.Id = newEntity.Id;
            return post;
        }

        public async Task<bool> DeleteCommentByIdAsync(int postId, int commentId)
        {
            var commentToDelete = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId && c.PostId == postId);
            if (commentToDelete != null)
            {
                _context.Remove(commentToDelete);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<bool> DeletePostByIdAsync(int id)
        {
                var postToDelete = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
                if (postToDelete != null)
                {
                    _context.Remove(postToDelete);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;

        }

        public Task<List<Domain.Post>> GetAllPosts()
        {
            try
            {
                return Task.FromResult(_context.Posts
                .Include(u => u.User)
                .ThenInclude(c => c.Comments)
                .Select(p => new Domain.Post
                {
                    Id = p.Id,
                    UserId = p.User.Id,
                    Username = p.User.Username,
                    Image = p.Image,
                    Created = p.Created,
                    Title = p.Title,
                    Body = p.Body,
                    Comments = p.Comments.Select(k => new Domain.Comment(k.Id, k.UserId, k.PostId, k.User.Username, k.Created, k.CommentBody)).ToList()
                }).ToList());
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(new List<Domain.Post>());
            }
            
            //throw new NotImplementedException();
        }

        public Task<List<Domain.FriendPosts>> GetFriendsPosts(int userId)
        {
            /*var foundFriends = _context.Followers.Where(u => u.UserId == userId && u.FriendRequest == 1).Include(fu => fu.UserId2Navigation).ThenInclude(p => p.Posts).ThenInclude(c => c.Comments).Select(p => new Domain.User
            {
                Posts = p.UserId2Navigation.Posts.Select(k => new Domain.Post(k.Id, k.UserId, k.User.Username, k.Image, k.Created, k.Title, k.Body, k.Comments.Select(k => new Domain.Comment(k.Id, k.UserId, k.PostId, k.User.Username, k.Created, k.CommentBody)).ToList())).ToList()
            }).ToList();
            Task<List<Domain.Post>> posts = foundFriends;
            return posts;*/
            try
            {
                var friendPosts = _context.Followers.Where(u => u.UserId == userId && u.FriendRequest == 1).Include(fp => fp.UserId2Navigation.Posts).ThenInclude(c => c.Comments).Select(p => new FriendPosts
                {
                    Posts = p.UserId2Navigation.Posts.Select(k => new Domain.Post(k.Id, k.UserId, k.User.Username, k.Image, k.Created, k.Title, k.Body, k.Comments.Select(k => new Domain.Comment(k.Id, k.UserId, k.PostId, k.User.Username, k.Created, k.CommentBody)).ToList())).ToList()
                }).ToList();

                return Task.FromResult(friendPosts);
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(new List<Domain.FriendPosts>());
            }
            

            
        }

        public Task<Domain.Post> GetPostById(int id)
        {
            try
            {
                var returnedPosts = _context.Posts
                .Include(u => u.User)
                .ThenInclude(c => c.Comments)
                .Select(p => new Domain.Post
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    Username = p.User.Username,
                    Image = p.Image,
                    Created = p.Created,
                    Title = p.Title,
                    Body = p.Body,
                    Comments = p.Comments.Select(k => new Domain.Comment(k.Id, k.UserId, k.PostId, k.User.Username, k.Created, k.CommentBody)).ToList()
                }).ToList();

                Domain.Post singlePost = returnedPosts.FirstOrDefault(p => p.Id == id);
                return Task.FromResult(singlePost);
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(new Domain.Post());
            }
            

        }

        public async Task<Domain.Comment> UpdateCommentById(int commentId, Domain.Comment comment)
        {
            Entities.Comment foundComment = await _context.Comments.FindAsync(commentId);
            if (foundComment != null)
            {
                foundComment.CommentBody = comment.CommentBody;
                _context.Comments.Update(foundComment);
                await _context.SaveChangesAsync();
                return new Domain.Comment(foundComment.Id, foundComment.UserId, foundComment.PostId, foundComment.Created, foundComment.CommentBody);
            }
            return new Domain.Comment();
            /*var updatePost = new Entities.Post
            {
                Id = id,
                Image = post.Image,
                Title = post.Title,
                Body = post.Body
            };
            
            return Task.FromResult(post);*/
        }

        public async Task<Domain.Post> UpdatePostById(int id, Domain.Post post)
        {

            Entities.Post foundPost = await _context.Posts.FindAsync(id);
            if (foundPost != null)
            {
                foundPost.Title = post.Title;
                foundPost.Body = post.Body;
                foundPost.Image = post.Image;
                _context.Posts.Update(foundPost);
                await _context.SaveChangesAsync();

                var updatedPost = await GetPostById(id);

                return updatedPost;
            }

            return new Domain.Post();
        }
    }
}
