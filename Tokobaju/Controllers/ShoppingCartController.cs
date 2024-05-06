using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tokobaju.Dto;
using Tokobaju.Exceptions;
using Tokobaju.Services;

namespace Tokobaju.Controllers;

[Route("/api/v1/shoppingCarts")]
public class ShoppingCartController : Controller
{
    private readonly IShoppingCartItemService _shoppingCartItemService;

    public ShoppingCartController(IShoppingCartItemService shoppingCartItemService)
    {
        _shoppingCartItemService = shoppingCartItemService;
    }

    [HttpPost, Authorize(Roles = "User")]
    public async Task<IActionResult> AddShoppingCartItem([FromBody] ShoppingCartItemDto payload)
    {
        if (payload.ProductId == "")
        {
            throw new BadRequestException("productId required");
        }
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var data = await _shoppingCartItemService.Create(userId, payload);
        var response = new ResponseDto
        {
            Message = "add data success",
            Data = data
        };

        return Created("/api/v1/shoppingCarts", response);
    }

    [HttpGet, Authorize(Roles = "User")]
    public async Task<IActionResult> GetShoppingCartItemsByUserId()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var data = await _shoppingCartItemService.GetByUserId(userId);
        var response = new ResponseDto
        {
            Message = "get data success",
            Data = data
        };

        return Ok(response);
    }

    [HttpPatch("{cartItemId}"), Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateShoppingCartItem(string cartItemId, [FromBody] ShoppingCartItemDto payload)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var data = await _shoppingCartItemService.Update(userId, cartItemId, payload.Quantity);
        var response = new ResponseDto
        {
            Message = "update data success",
            Data = data
        };

        return Ok(response);
    }

    [HttpDelete("{cartItemId}"), Authorize(Roles = "User")]
    public async Task<IActionResult> DeleteShoppingCartItem(string cartItemId)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        await _shoppingCartItemService.Delete(userId, cartItemId);
        var response = new ResponseDto
        {
            Message = "update data success",
            Data = $"data with id {cartItemId} deleted"
        };

        return Ok(response);
    }
}