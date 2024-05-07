using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tokobaju.Dto;
using Tokobaju.Entities;
using Tokobaju.Exceptions;
using Tokobaju.Services;

namespace Tokobaju.Controllers;

[Route("/api/v1/stores")]
public class StoreController : Controller
{
    private readonly IStoreService _storeService;

    public StoreController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpPost, Authorize(Roles = "User")]
    public async Task<IActionResult> CreateStore(IFormFile? photo, [FromForm] StoreDto payload)
    {
        if (payload.Name == null || payload.Description == null || payload.Name == "" || payload.Description == "")
        {
            throw new BadRequestException("name & description can't be empty");
        }

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
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Stores");
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

        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var data = await _storeService.Create(userId, photoPath, payload);
        var response = new ResponseDto
        {
            Message = "create data success",
            Data = data
        };

        if (photo != null)
        {
            using (var stream = new FileStream(photoPath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }
        }

        return Created("/api/v1/stores", response);
    }

    [HttpGet("/users"), Authorize(Roles = "User")]
    public async Task<IActionResult> GetStoreByUserId()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var data = await _storeService.GetByUserId(userId)!;
        if (data == null)
        {
            throw new NotFoundException($"user with id {userId} doesnt have store");
        }

        var response = new ResponseDto
        {
            Message = "get data success",
            Data = data
        };

        return Ok(response);
    }

    [HttpGet, Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> GetAllStores()
    {
        var data = await _storeService.GetAll();
        var response = new ResponseDto
        {
            Message = "get data success",
            Data = data
        };

        return Ok(response);
    }

    [HttpPut, Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateStore(IFormFile? photo, [FromForm] StoreDto? payload)
    {
        if (photo == null && payload == null)
        {
            throw new BadRequestException("nothing to change");
        }

        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var photoPath = "";
        if (photo != null)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Stores");
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

        Store data;
        if (payload == null)
        {
            data = await _storeService.Update(userId, photoPath, new StoreDto());
        } else 
        {
            data = await _storeService.Update(userId, photoPath, payload!);
        }

        var response = new ResponseDto
        {
            Message = "update data success",
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

    [HttpDelete, Authorize(Roles = "User")]
    public async Task<IActionResult> DeleteStore()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        await _storeService.Delete(userId);
        var response = new ResponseDto
        {
            Message = "delete data success",
            Data = $"store with id {userId} deleted"
        };

        return Ok(response);
    }
}