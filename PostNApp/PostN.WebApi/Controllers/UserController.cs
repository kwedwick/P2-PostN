using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostN.Domain;
using System;
using System.Threading.Tasks;
using PostN.WebApi.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

namespace PostN.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepo _repo;
        public UserController(ILogger<UserController> logger, IUserRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<User>> GetUsers()
        {
            var user = await _repo.GetUsers();
            return Ok(user);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _repo.GetUserById(id);
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreatedUser user)
        {

            try
            {
                var newUser = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Username = user.Username,
                    Password = user.Password,
                    AboutMe = user.AboutMe,
                    State = user.State,
                    Country = user.Country,
                    Role = "User",
                    PhoneNumber = user.PhoneNumber,
                    DoB = user.DoB

                };
                var returnedUSer = await _repo.AddAUser(newUser);
                return Ok(returnedUSer);
            }
            catch (Exception e)
            {
                //ModelState.AddModelError("Username", e.Message);
                //ModelState.AddModelError("Email", e.Message);
                _logger.LogCritical("Failed to create new user", e.Message);
                return BadRequest(e.Message);
            }

        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] UpdatedUser user)
        {
            try
            {
                User newUpdateUser = new()
                {
                    Id = id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    AboutMe = user.AboutMe
                };
                User updateUser = await _repo.UpdateUser(id, newUpdateUser);
                return Ok(updateUser);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool result = await _repo.DeleteUserById(id);
            if (result == false)
                return NotFound();
            return Ok();
        }

    }
}