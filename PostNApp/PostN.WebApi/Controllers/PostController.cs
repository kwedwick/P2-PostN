using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostN.Domain;
using PostN.WebApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace PostN.WebApi.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepo _postRepo;
        private readonly ILogger<PostController> _logger;

        public PostController(IPostRepo postRepo, ILogger<PostController> logger)
        {
            _postRepo = postRepo;
            _logger = logger;
        }
        // GET: api/post
        /// <summary>
        /// Get's all Posts with Comments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Post>> Get()
        {
            List<Post> Post = await _postRepo.GetAllPosts();
            Post.Reverse();
            return Ok(Post);
        }

        // GET api/post/5
        /// <summary>
        /// GET one post w/ comments by Post ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> Get(int id)
        {
            if (await _postRepo.GetPostById(id) is Post singlePost)
            {
                _logger.LogInformation($"Found PostId:{id}");
                return Ok(singlePost);
            }
            return NotFound();
        }

        // POST api/post
        /// <summary>
        /// Create a POST using Post body
        /// </summary>
        /// <param name="viewPost"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Post>> Post([FromBody] CreatedPost viewPost)
        {
            //need to check if user is logged in. need user's ID
            //return the new post information - route them to that specific post
            if(viewPost != null)
            {
                var post = new Post
                {
                    UserId = viewPost.UserId,
                    Image = viewPost.Image,
                    Created = DateTime.Now,
                    Title = viewPost.Title,
                    Body = viewPost.Body,
                    Username = viewPost.Username
                };
                try
                {
                    _logger.LogDebug($"New {post.Title}, {post.Username}");
                    Post newPost = await _postRepo.CreatePost(post);
                    return Ok(newPost);
                }
                catch (Exception e)
                {
                    _logger.LogCritical("Failed to create new post", e);
                    return NotFound(e);
                }
            }
            return NotFound();
        }

        
        // PUT api/post/5
        /// <summary>
        /// Update Post using Post URL ID and post body
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> Put(int id, [FromBody] Post post)
        {
            if (await _postRepo.GetPostById(id) is Post oldPost)
            {
                oldPost.Title = post.Title;
                oldPost.Image = post.Image;
                oldPost.Body = post.Body;

                Post updatedPost = await _postRepo.UpdatePostById(id, oldPost);
                return Ok(updatedPost);
            }
            _logger.LogCritical($"Unable to update Post, ID: {id}, with {post.Title} | {post.Image} | {post.Body} | information");
            return NotFound();
        }

        // DELETE api/post/5
        /// <summary>
        /// Delete post by POST ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(await _postRepo.DeletePostByIdAsync(id))
            {
                _logger.LogDebug($"Post ID: {id} was successfully deleted");
                return NoContent();
            }
            _logger.LogDebug($"Unable to find POST {id} to delete");
            return NotFound($"Post with ID: {id} not found!");
        }
        /// <summary>
        /// Create a comment using POST ID
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("{postId}/comment")]
        public async Task<ActionResult<Post>> Post(int postId, [FromBody] CreatedComment comment)
        {
            if(await _postRepo.GetPostById(postId) is Post post)
            {
                _logger.LogDebug($"Found post {postId} to create comment");
                var newComment = new Comment
                {
                    UserId = comment.UserId,
                    Username = comment.Username,
                    PostId = postId,
                    Created = DateTime.Now,
                    CommentBody = comment.CommentBody
                };
               newComment = await _postRepo.CreateCommentByPostId(postId, newComment);
                return Ok(newComment);
            }
            _logger.LogError($"Was not able to find post {postId} or something went wrong when creating {comment}");
            return NotFound();
        }

        /// <summary>
        /// Update Comment by Comment&Post ID
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="commentId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{postId}/comment/{commentId}")]
        public async Task<ActionResult<Comment>> Put(int postId, int commentId, [FromBody] Comment comment)
        {
            // find post ID, then comment ID, then update comment
            if (await _postRepo.GetPostById(postId) is Post post)
            {
                _logger.LogInformation($"Found post {postId}");
                Comment updatedComment = await _postRepo.UpdateCommentById(commentId, comment);
                _logger.LogInformation($"Successfuly created comment with ID: {updatedComment.Id}");
                return Ok(updatedComment);
            }
            _logger.LogError($"Was not able to find post {postId} or something went wrong when updating {comment}");
            return NotFound();
        }

        /// <summary>
        /// Delete Comment by Comment&Post ID - API Route
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="commentId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{postId}/comment/{commentId}")]
        public async Task<IActionResult> Delete(int postId, int commentId)
        {
            /*if (await _postRepo.GetPostById(postId) is Post post)
            {
                await _postRepo.DeleteCommentByIdAsync(commentId);
                return Ok(post);
            }
            return NotFound();*/


            if(await _postRepo.DeleteCommentByIdAsync(postId, commentId))
            {
                _logger.LogDebug($"Successfully deleted comment {commentId} under Post {postId}");
                return NoContent();
            }
            _logger.LogError($"Post with ID: {postId} and Comment ID: {commentId} not found!");
            return NotFound($"Post with ID: {postId} and Comment ID: {commentId} not found!");
        }
       
        [HttpGet("{userId}/[action]/friends/")]
        public async Task<ActionResult<Post>> GetFriendPosts(int userId)
        {
           
           
            var Post = await _postRepo.GetFriendsPosts(userId);

            List<Post> homeposts = new();

            foreach(FriendPosts friendpost in Post)
            {
                homeposts.AddRange(friendpost.Posts);
            }
            homeposts.Reverse();
            return Ok(homeposts);
        }

    }
}
