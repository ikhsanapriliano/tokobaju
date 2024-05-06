using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tokobaju.Dto;
using Tokobaju.Services;

namespace Tokobaju.Controllers;

[Route("/api/v1/shippers")]
public class ShipperController : Controller
{
    private readonly IShipperService _shipperService;

    public ShipperController(IShipperService shipperService)
    {
        _shipperService = shipperService;
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateShipper([FromBody] ShipperDto payload)
    {
        var data = await _shipperService.Create(payload);
        var response = new ResponseDto
        {
            Message = "create data success",
            Data = data
        };

        return Created("/api/v1/shippers", response);
    }

    [HttpGet("{id}"), Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> GetByShipperById(string id)
    {
        var data = await _shipperService.GetById(id);
        var response = new ResponseDto
        {
            Message = "get data success",
            Data = data
        };

        return Ok(response);
    }

    [HttpGet, Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> GetAllShippers()
    {
        var data = await _shipperService.GetAll();
        var response = new ResponseDto
        {
            Message = "get data success",
            Data = data
        };

        return Ok(response);
    }

    [HttpPatch("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateShipper(string id, [FromBody] ShipperDto payload)
    {
        var data = await _shipperService.Update(id, payload);
        var response = new ResponseDto
        {
            Message = "update data success",
            Data = data
        };

        return Ok(response);
    }

    [HttpDelete("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteShipper(string id)
    {
        await _shipperService.Delete(id);
        var response = new ResponseDto
        {
            Message = "delete data success",
            Data = $"shipper with id {id} deleted"
        };

        return Ok(response);
    }
}