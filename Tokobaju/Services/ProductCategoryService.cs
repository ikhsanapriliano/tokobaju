using Tokobaju.Dto;
using Tokobaju.Entities;
using Tokobaju.Exceptions;
using Tokobaju.Repositories;

namespace Tokobaju.Services;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IRepository<ProductCategory> _repository;
    private readonly IPersistence _persistence;

    public ProductCategoryService(IRepository<ProductCategory> repository, IPersistence persistence)
    {
        _repository = repository;
        _persistence = persistence;
    }

    public async Task<ProductCategory> Create(ProductCategoryDto payload)
    {
        var productCategory = new ProductCategory
        {
            Name = payload.Name,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        var data = await _repository.SaveAsync(productCategory);
        await _persistence.SaveChangesAsync();

        return data;
    }

    public async Task Delete(string id)
    {
        var productCategory = await GetById(id);
        _repository.Delete(productCategory);
        await _persistence.SaveChangesAsync();
    }

    public async Task<List<ProductCategory>> GetAll()
    {
        var productCategories = await _repository.FindAllAsync();

        return productCategories;
    }

    public async Task<ProductCategory> GetById(string id)
    {
        if (!Guid.TryParse(id, out var result))
        {
            throw new Exception("invalid id");
        }

        var productCategory = await _repository.FindByIdAsync(Guid.Parse(id));

        if (productCategory == null)
        {
            throw new NotFoundException($"productCategory with id {id} not found");
        }

        return productCategory;
    }

    public async Task<ProductCategory> Update(string id, ProductCategoryDto payload)
    {
        var productCategory = await GetById(id);
        productCategory.Name = payload.Name;
        productCategory.UpdatedAt = DateTime.Now;

        var newProductCategory = _repository.Update(productCategory);
        await _persistence.SaveChangesAsync();

        return newProductCategory;
    }
}