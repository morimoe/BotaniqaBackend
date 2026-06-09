using System.ComponentModel.DataAnnotations;

namespace Botaniqa.BL.UserDTO
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Никнейм обязателен")]
        [MinLength(3, ErrorMessage = "Никнейм минимум 3 символа")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Почта обязательна")]
        [EmailAddress(ErrorMessage = "Некорректный формат почты")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(6, ErrorMessage = "Пароль минимум 6 символов")]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }

    public class UpdateMeRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
