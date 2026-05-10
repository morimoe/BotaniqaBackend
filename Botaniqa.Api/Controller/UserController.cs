using AutoMapper;
using Botaniqa.BL.UserDTO;
using Botaniqa.DataAccess.Context;
using Botaniqa.Domain.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        [HttpGet("all")]
        [AdminMod]
        public IActionResult GetAllUsers()
        {
            return Ok(_context.Users.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound(new { Message = $"User with ID {id} not found" });
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
        [AdminMod]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
                return NotFound(new { Message = $"User with ID {id} not found" });
            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;
            _context.SaveChanges();
            return Ok(existingUser);
        }


        [HttpDelete("{id}")]
        [AdminMod]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound(new { Message = $"User with ID {id} not found" });
            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // ищем пользователя в базе
            var user = _context.Users.FirstOrDefault(u =>
                u.Username == login.Credential && u.Password == login.Password);

            if (user == null)
                return Unauthorized(new { Message = "Invalid credentials" });

            // генерируем токен с ролью
            var tokenService = new BusinessLogic.TokenService();
            var token = tokenService.GenerateToken(user.Id, user.Username, user.Role);

            return Ok(new { Token = token });
        }
    }
}
