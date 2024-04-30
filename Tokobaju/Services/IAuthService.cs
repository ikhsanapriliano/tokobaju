using Tokobaju.Dto;

namespace Tokobaju.Services;

public interface IAuthService
{
    Task<RegisterResponseDto> Register(string photo, RegisterRequestDto payload);
    Task<LoginResponseDto> Login(LoginRequestDto payload);
}