using Tokobaju.Dto;
using Tokobaju.Entities;

namespace Tokobaju.Services;

public interface IProductService
{
    Task<Product> Create(string userId, string photo, ProductDto payload);
    Task<Product> GetById(string id);
    Task<List<Product>> GetByStoreId(string storeId);
    Task<List<Product>> GetAll();
    Task<Product> Update(string id, string userId, string photo, ProductDto payload);
    Task<bool> Delete(string id, string userId);
}