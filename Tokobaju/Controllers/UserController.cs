using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tokobaju.Dto;
using Tokobaju.Enums;
using Tokobaju.Exceptions;
using Tokobaju.Services;

namespace Tokobaju.Controllers;

[Route("/api/v1/users")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}"), Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var role = identity!.FindFirst(ClaimTypes.Role)!.Value;
        if (role == ERole.User.ToString() && id != userId)
        {
            throw new ForbiddenResourceException("forbidden resource");
        }

        var data = await _userService.GetById(id);
        data.Password = "";
        var response = new ResponseDto
        {
            Message = "get data success",
            Data = data
        };

        return Ok(response);
    }

    [HttpGet, Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var data = await _userService.GetAll();
        var response = new ResponseDto
        {
            Message = "get data success",
            Data = data
        };

        return Ok(response);
    }

    [HttpPut, Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> UpdateUser(IFormFile? photo, [FromForm] UpdateUserDto payload)
    {
        if (photo == null && payload.Name == null && payload.Email == null)
        {
            throw new BadRequestException("nothing to change");
        }

        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var photoPath = "";
        if (photo != null)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Users");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var extensions = new[] {".png", ".jpg", ".jpeg"};
            var fileExtension = Path.GetExtension(photo.FileName).ToLower();
            if (!extensions.Contains(fileExtension))
            {  
                throw new BadRequestException("format file should be: '.png', '.jpg', 'jpeg'"); 
            }

            var random = new Random();

            photoPath = Path.Combine(folderPath, random.Next(00000000, 99999999).ToString() + fileExtension);
        }

        var data = await _userService.Update(userId, photoPath, payload!);
        var response = new ResponseDto
        {
            Message = "update success",
            Data = data
        };

        if (photo != null)
        {
            using (var stream = new FileStream(photoPath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }
        }

        return Ok(response);
    }

    [HttpPatch, Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto payload)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var result = await _userService.ChangePassword(userId, payload);
        if (result == false)
        {
            throw new Exception("change password failed");
        }
        var response = new ResponseDto
        {
            Message = "update data success",
            Data = $"password changed for user with id {userId}"
        };

        return Ok(response);
    }

    [HttpDelete, Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> DeleteUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var result = await _userService.Delete(userId);
        if (result == false)
        {
            throw new Exception("delete data failed");
        }
        var response = new ResponseDto
        {
            Message = "delete data success",
            Data = $"data with id {userId} deleted"
        };

        return Ok(response);
    }
}