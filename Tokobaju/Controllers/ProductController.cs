using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tokobaju.Dto;
using Tokobaju.Exceptions;
using Tokobaju.Services;

namespace Tokobaju.Controllers;

[Route("/api/v1/products")]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost, Authorize(Roles = "User")]
    public async Task<IActionResult> CreateProduct(IFormFile photo, [FromForm] ProductDto payload)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Products");
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
        var photoPath = Path.Combine(folderPath, random.Next(00000000, 99999999).ToString() + fileExtension);

        if (payload.Name == "" || payload.Description == "" || payload.Price == 0 || payload.Stock == 0)
        {
            throw new BadRequestException("name, description, price, stock required");
        }

        var data = await _productService.Create(userId, photoPath, payload);
        var response = new ResponseDto
        {
            Message = "create data success",
            Data = data
        };

        using (var stream = new FileStream(photoPath, FileMode.Create))
        {
            await photo.CopyToAsync(stream);
        }

        return Created("/api/v1/products", response);
    }  

    [HttpGet("{id}"), Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> GetProductById(string id)
    {
        var data = await _productService.GetById(id);
        var response = new ResponseDto
        {
            Message = "get data success",
            Data = data
        };

        return Ok(response);
    }

    [HttpGet("/stores/{storeId}"), Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> GetProductsByStoreId(string userId)
    {
        var data = await _productService.GetByStoreId(userId);
        var response = new ResponseDto
        {
            Message = "get data success",
            Data = data
        };

        return Ok(response);
    }

    [HttpGet, Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> GetAllProducts()
    {
        var data = await _productService.GetAll();
        var response = new ResponseDto
        {
            Message = "get data success",
            Data = data
        };

        return Ok(response);
    }

    [HttpPatch("{id}"), Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateProduct(string id, IFormFile? photo, [FromForm] ProductDto? payload)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var photoPath = "";
        if (photo != null)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Products");
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

        if (payload == null)
        {
            payload = new ProductDto();
        }

        var data = await _productService.Update(id, userId, photoPath, payload);
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

    [HttpDelete("{id}"), Authorize(Roles = "User")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        
        var data = await _productService.Delete(id, userId);
        var response = new ResponseDto
        {
            Message = "delete data success",
            Data = data
        };

        return Ok(response);
    }
}