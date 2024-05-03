using Tokobaju.Dto;
using Tokobaju.Entities;
using Tokobaju.Enums;
using Tokobaju.Exceptions;
using Tokobaju.Repositories;
using Tokobaju.Utils;

namespace Tokobaju.Services;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _repository;
    private readonly IPersistence _persistence;
    private readonly BcryptUtil _bcryptUtil;
    private readonly IJwtUtil _JwtUtil;
    private readonly IShoppingCartService _shoppingCartService;

    public AuthService(IRepository<User> repository, IPersistence persistence, BcryptUtil bcryptUtil, IJwtUtil jwtUtil, IShoppingCartService shoppingCartService)
    {
        _repository = repository;
        _persistence = persistence;
        _bcryptUtil = bcryptUtil;
        _JwtUtil = jwtUtil;
        _shoppingCartService = shoppingCartService;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto payload)
    {
        var user = await _repository.FindAsync(user => user.Email.Equals(payload.Email));
        if (user == null)
        {
            throw new NotFoundException($"user with email {payload.Email} not found");
        }
        var validate = _bcryptUtil.Validate(payload.Password, user.Password!);
        if (!validate)
        {
            throw new UnauthorizedException("wrong password");
        }

        var token = _JwtUtil.GenerateToken(user);

        var loginResponse = new LoginResponseDto
        {
            AccessToken = token
        };

        return loginResponse;
    }

    public async Task<RegisterResponseDto> Register(string photo, RegisterRequestDto payload)
    {
        await _persistence.BeginTransactionAsync();
        try
        {   
            if (payload.Password != payload.ConfirmPassword)
            {
                throw new BadRequestException("password and confirmPassword didn't match");
            }

            payload.Password = _bcryptUtil.HashPassword(payload.Password);

            var user = new User
            {
                Name = payload.Name,
                Email = payload.Email,
                Password = payload.Password,
                Role = Enum.Parse<ERole>("User", true),
                Photo = photo,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var data = await _repository.SaveAsync(user);
            await _persistence.SaveChangesAsync();

            await _shoppingCartService.Create(data.Id.ToString());
            await _persistence.CommitTransactionAsync();

            var registerResponse = new RegisterResponseDto
            {
                Id = data.Id,
                Name = data.Name,
                Email = data.Email,
                Role = data.Role
            };

            return registerResponse;
        }
        catch (Exception)
        {
            await _persistence.RollbackTransactionAsync();
            throw;
        }
    }
}