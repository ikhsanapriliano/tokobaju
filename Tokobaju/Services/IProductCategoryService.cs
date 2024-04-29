using Tokobaju.Dto;
using Tokobaju.Entities;

namespace Tokobaju.Services;

public interface IProductCategoryService 
{
    Task<ProductCategory> Create(ProductCategoryDto payload);
    Task<ProductCategory> GetById(string id);
    Task<List<ProductCategory>> GetAll();
    Task<ProductCategory> Update(string id, ProductCategoryDto payload);
    Task Delete(string id);
}