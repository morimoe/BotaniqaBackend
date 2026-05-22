using AutoMapper;
using Botaniqa.BL.UserDTO;
using Botaniqa.DataAccess.Context;
using Botaniqa.Domain.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

            if (!string.IsNullOrEmpty(updatedUser.Username)) existingUser.Username = updatedUser.Username;
            if (!string.IsNullOrEmpty(updatedUser.Email)) existingUser.Email = updatedUser.Email;
            if (!string.IsNullOrEmpty(updatedUser.Role)) existingUser.Role = updatedUser.Role;

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
                (u.Username == login.Credential || u.Email == login.Credential)
                && u.Password == login.Password);

            if (user == null)
                return Unauthorized(new { Message = "Invalid credentials" });

            // генерируем токен с ролью
            var tokenService = new BusinessLogic.TokenService();
            var token = tokenService.GenerateToken(user.Id, user.Username, user.Role);

            return Ok(new { Token = token });
        }

        [HttpPut("me")]
        [Authorize]
        public IActionResult UpdateMe([FromBody] UpdateMeRequest request)
        {
            // достаём id из JWT токена
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return NotFound();

            if (!string.IsNullOrEmpty(request.Username)) user.Username = request.Username;
            if (!string.IsNullOrEmpty(request.Email)) user.Email = request.Email;
            if (!string.IsNullOrEmpty(request.Password)) user.Password = request.Password;

            _context.SaveChanges();
            return Ok(new { Message = "Данные обновлены" });
        }

    }
}
