using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tokobaju.Dto;
using Tokobaju.Services;

namespace Tokobaju.Controllers;

[Route("/api/v1/productCategories")]
public class ProductCategoryController : Controller
{
    private readonly IProductCategoryService _productCategoryService;

    public ProductCategoryController(IProductCategoryService productCategoryService)
    {
        _productCategoryService = productCategoryService;
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateProductCategory([FromBody] ProductCategoryDto payload)
    {
        var data = await _productCategoryService.Create(payload);
        var response = new ResponseDto
        {
            Message = "create data success",
            Data = data
        };

        return Created("/api/v1/productCategories", response);
    }

    [HttpGet("{id}"), Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> GetProductCategoryById(string id)
    {  
        var data = await _productCategoryService.GetById(id);
        var response = new ResponseDto
        {
            Message = "get data success",
            Data = data
        };

        return Ok(response);
    }

    [HttpGet, Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> GetAllProductCategories()
    {
        var data = await _productCategoryService.GetAll();
        var response = new ResponseDto
        {
            Message = "get data success",
            Data = data
        };

        return Ok(response);
    }

    [HttpPut("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProductCategory(string id, [FromBody] ProductCategoryDto payload)
    {
        var data = await _productCategoryService.Update(id, payload);
        var response = new ResponseDto
        {
            Message = "update data success",
            Data = data
        };

        return Ok(response);
    }

    [HttpDelete("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProductCategory(string id)
    {
        await _productCategoryService.Delete(id);
        var response = new ResponseDto
        {
            Message = "delete data success",
            Data = $"data with id {id} deleted"
        };

        return Ok(response);
    }
}