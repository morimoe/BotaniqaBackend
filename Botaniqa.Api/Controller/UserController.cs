using AutoMapper;
using Botaniqa.BL.UserDTO;
using Botaniqa.Domain.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Botaniqa.DataAccess.Context;

namespace Botaniqa.Api.Controller

{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly BusinessLogic.Interfaces.ISession _session;
        
        private readonly IMapper _mapper;

        private readonly UserContext _context;

        public UserController(IMapper mapper, UserContext context)
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _mapper = mapper;
            _context = context;
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
            
            var user = _mapper.Map<UserData>(request);
           
            _context.Users.Add(user);

            _context.SaveChanges();
            
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
