using System.ComponentModel.DataAnnotations;

namespace Tokobaju.Dto;

public class RegisterRequestDto
{
    [Required(ErrorMessage = "name required")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "email required")]
    [EmailAddress(ErrorMessage = "invalid email")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "password required")]
    [MinLength(6, ErrorMessage = "password min length 6")]
    public string Password { get; set; } = "";

    [Required(ErrorMessage = "confirmPassword required")]
    [MinLength(6, ErrorMessage = "confirmPassword min length 6")]
    public string ConfirmPassword { get; set; } = "";
}