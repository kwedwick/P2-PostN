using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostN.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace PostN.WebApi.Controllers
{
    [Route("api/friends")]
    [ApiController]
    public class FollowerController : ControllerBase
    {
        private readonly ILogger<FollowerController> _logger;
        private readonly IUserRepo _repo;
        public FollowerController(ILogger<FollowerController> logger, IUserRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }

        // GET api/<FollowerController>/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<Follower>> Get(int userId)
        {
            var followers = await _repo.GetFollowers(userId);
            return Ok(followers);
        }
        [HttpGet("[action]/{userId}/{friendId}")]
        public async Task<ActionResult<bool>> CheckIfFriend(int userId, int friendId)
        {
            bool status = await _repo.CheckIfFriend(userId, friendId);
            return Ok(status);
        }


        // put api/<FollowerController>
        [HttpPost("{userId}/{friendId}")]
        public async Task<ActionResult<bool>> Put(int userId, int friendId)
        {
            
        
                bool newFollower = await _repo.AddAFollower(userId, friendId);
                return Ok(newFollower);

        }

        // DELETE api/<FollowerController>/5
        [HttpDelete("{userId}/{friendId}")]
        public async Task<IActionResult> Delete(int userId, int friendId)
        {
            var x = await _repo.DeleteFollower(userId, friendId);
            if(x == false)
            {
                return NotFound(false);
            }
            return Ok(true);
        }

    }
}
