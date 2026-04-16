using Botaniqa.Api.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography.Xml;

namespace Botaniqa.Api.Controller
{
    [Route(template: "api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // In-memory storage for users (for demonstration purposes)
        private static List<User> _users = new();

        private static int _nextId = 1;

        [HttpGet(template: "all")]
        public IActionResult GetAllUsers()
        {
            return Ok(_users);
        }

        [HttpGet(template: "{id}")]

        public IActionResult GetUserById(int id)
        {

            var user = _users.FirstOrDefault( u  => u.Id == id);

            if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} not found" });
            }

             return Ok(user);
        }
        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserRequest request)
        {
            var user = new User
            {
                Id = _nextId++,
                Username = request.Username,
                Email = request.Email
            };

            _users.Add(user);
            return Created($"api/users/{user.Id}", user);
        }

        [HttpPut(template: "{id}")]

        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                return NotFound(new { Message = $"User with ID {id} not found" });
            }
            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;

            return Ok(existingUser);
        }


        [HttpDelete(template: "{id}")]

        public IActionResult DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} not found" });
            }
            _users.Remove(user);

            return NoContent();
        }
    }
}
