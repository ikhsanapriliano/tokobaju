using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tokobaju.Dto;
using Tokobaju.Services;

namespace Tokobaju.Controllers;

[Route("/api/v1/orders")]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto payload)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var data = await _orderService.Create(userId, payload);
        var response = new ResponseDto
        {
            Message = "create data success",
            Data = data
        };

        return Created("/api/v1/orders", response);
    }

    [HttpGet("{id}"), Authorize(Roles = "User")]
    public async Task<IActionResult> GetOrderById(string id)
    {
        var data = await _orderService.GetById(id);
        var response = new ResponseDto
        {
            Message = "get data success",
            Data = data
        };

        return Ok(response);
    }

    [HttpGet(), Authorize(Roles = "User")]
    public async Task<IActionResult> GetOrdersByUserId()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var data = await _orderService.GetByUserId(userId);
        var response = new ResponseDto
        {
            Message = "get data success",
            Data = data
        };

        return Ok(response);
    }

    [HttpPatch("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> PayOrder(string Id)
    {
        var data = await _orderService.Update(Id);
        var response = new ResponseDto
        {
            Message = "update data success",
            Data = data
        };

        return Ok(response);
    }
}