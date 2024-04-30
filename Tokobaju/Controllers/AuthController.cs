using Microsoft.AspNetCore.Mvc;
using Tokobaju.Dto;
using Tokobaju.Exceptions;
using Tokobaju.Services;

namespace Tokobaju.Controllers;

[ApiController]
[Route("/api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(IFormFile? photo, [FromForm] RegisterRequestDto payload)
    {
        string photoName;
        switch (photo)
        {
            case null:
                photoName = "unknown.jpg";
                break;
            case not null:
                photoName = photo.FileName;
                break;
        }
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var extensions = new[] {".png", ".jpg", ".jpeg"};
        var fileExtension = Path.GetExtension(photoName).ToLower();
        if (!extensions.Contains(fileExtension))
        {  
            throw new BadRequestException("format file should be: '.png', '.jpg', 'jpeg'"); 
        }

        var random = new Random();

        var photoPath = Path.Combine(folderPath, random.Next(00000000, 99999999).ToString() + (photo != null ? fileExtension : "-" + photoName));

        var data = await _authService.Register(photoPath, payload);
        var response = new ResponseDto
        {
            Message = "register success",
            Data = data
        };

        if (photo != null)
        {
            using (var stream = new FileStream(photoPath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }
        }

        return Created("/api/v1/auth/register", response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequestDto payload)
    {
        var data = await _authService.Login(payload);
        var response = new ResponseDto
        {
            Message = "login success",
            Data = data
        };

        return Ok(response);
    }
}