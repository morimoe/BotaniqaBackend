using Botaniqa.Api.Domain;
using Botaniqa.Domain.Entities.User;
using eUseControl.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace Botaniqa.Api.Controller
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Botaniqa.BusinessLogic.Interfaces.ISession _session; // ← полный путь
        public UserController()
        {
            var bl = new BussinesLogic();
            _session = bl.GetSessionBL();
        }

        // In-memory storage for users (for demonstration purposes)
        private static List<User> _users = new();
        private static int _nextId = 1;

        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            return Ok(_users);
        }

        [HttpGet("{id}")]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ULoginData data = new ULoginData
            {
                Credential = login.Credential,
                Password = login.Password,
                LoginIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
                LoginDateTime = DateTime.Now
            };

            var result = _session.UserLogin(data);

            if (result.Status)
            {
                return Ok(new { Message = "Login successful" });
            }
            else
            {
                return Unauthorized(new { Message = result.StatusMsg });
            }
        }
    }
}
