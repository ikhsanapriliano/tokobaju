using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Tokobaju.Entities;

namespace Tokobaju.Utils;

public class JwtUtil : IJwtUtil
{
    private readonly IConfiguration _configuration;

    public JwtUtil (IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User payload)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration["JwtSettings:Issuer"],
            Expires = DateTime.Now.AddHours(int.Parse(_configuration["JwtSettings:ExpiresInMinutes"]!)),
            IssuedAt = DateTime.Now,
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, payload.Id.ToString()),
                new(ClaimTypes.Role, payload.Role.ToString())
            }),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}