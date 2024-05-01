using System.ComponentModel.DataAnnotations;

namespace Tokobaju.Dto;

public class UpdateUserDto
{
    public string? Name { get; set; }

    [EmailAddress(ErrorMessage = "invalid email")]
    public string? Email { get; set; }
}