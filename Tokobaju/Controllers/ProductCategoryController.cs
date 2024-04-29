using Microsoft.AspNetCore.Mvc;
using Tokobaju.Dto;
using Tokobaju.Services;

namespace Tokobaju.Controllers;

[ApiController]
[Route("/api/v1/productCategories")]
public class ProductCategoryController : ControllerBase
{
    private readonly IProductCategoryService _productCategoryService;

    public ProductCategoryController(IProductCategoryService productCategoryService)
    {
        _productCategoryService = productCategoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductCategory([FromBody] ProductCategoryDto payload)
    {
        var data = await _productCategoryService.Create(payload);

        return Created("/api/v1/productCategories", data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductCategoryById(string id)
    {  
        var data = await _productCategoryService.GetById(id);

        return Ok(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductCategories()
    {
        var data = await _productCategoryService.GetAll();

        return Ok(data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductCategory(string id, [FromBody] ProductCategoryDto payload)
    {
        var data = await _productCategoryService.Update(id, payload);

        return Ok(data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductCategory(string id)
    {
        await _productCategoryService.Delete(id);

        return Ok($"data with id {id} deleted");
    }
}