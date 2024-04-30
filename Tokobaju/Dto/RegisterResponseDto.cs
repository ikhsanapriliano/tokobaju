using Tokobaju.Enums;

namespace Tokobaju.Dto;

public class RegisterResponseDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required ERole Role { get; set; }
}