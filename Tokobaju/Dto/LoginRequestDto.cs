using System.ComponentModel.DataAnnotations;

namespace Tokobaju.Dto;

public class LoginRequestDto
{
    [Required(ErrorMessage = "Email required")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Password required")]
    [MinLength(6, ErrorMessage = "Password min length 6")]
    public string Password { get; set; } = "";
}