using System.ComponentModel.DataAnnotations;
public class UserLogin
{
    [Required]
    [Display(Name = "Username or Email")]
    public string Credential { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

}