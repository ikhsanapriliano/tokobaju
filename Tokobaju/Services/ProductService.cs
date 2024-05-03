using Microsoft.AspNetCore.Http.HttpResults;
using Tokobaju.Dto;
using Tokobaju.Entities;
using Tokobaju.Exceptions;
using Tokobaju.Repositories;

namespace Tokobaju.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _repository;
    private readonly IPersistence _persistence;
    private readonly IStoreService _storeService;
    private readonly IProductCategoryService _productCategoryService;

    public ProductService(IRepository<Product> repository, IPersistence persistence, IStoreService storeService, IProductCategoryService productCategoryService)
    {
        _repository = repository;
        _persistence = persistence;
        _storeService = storeService;
        _productCategoryService = productCategoryService;
    }
    
    public async Task<Product> Create(string userId, string photo, ProductDto payload)
    {
        var store = await _storeService.GetByUserId(userId)!;
        if (store == null)
        {
            throw new NotFoundException($"user with id {userId} doesn't have a store");
        }

        var productCategory = await _productCategoryService.GetById(payload.CategoryId);

        var input = new Product
        {
            Name = payload.Name,
            Description = payload.Description,
            Price = payload.Price,
            Stock = payload.Stock,
            Photo = photo,
            CategoryId = productCategory.Id,
            StoreId = store.Id,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var data = await _repository.SaveAsync(input);
        await _persistence.SaveChangesAsync();

        return data;
    }

    public async Task<bool> Delete(string id, string userId)
    {
        var product = await GetById(id);

        var store = await _storeService.GetByUserId(userId)!;
        if (store == null)
        {
            throw new NotFoundException($"user with id {userId} doesn't have a store");
        }

        if (product.StoreId != store.Id)
        {
            throw new ForbiddenResourceException("forbidden resource");
        }

        _repository.Delete(product);
        await _persistence.SaveChangesAsync();

        return true;
    }

    public async Task<List<Product>> GetAll()
    {
        var data = await _repository.FindAllAsync();

        return data;
    }

    public async Task<Product> GetById(string id)
    {
        var data = await _repository.FindByIdAsync(Guid.Parse(id));
        if (data == null)
        {
            throw new NotFoundException($"product with id {id} not found");
        }

        return data!;
    }

    public async Task<List<Product>> GetByStoreId(string storeId)
    {
        var data = await _repository.FindAllAsync(store => store.Id.Equals(storeId));

        return data;
    }

    public async Task<Product> Update(string id, string userId, string photo, ProductDto payload)
    {
        var product = await GetById(id);

        var store = await _storeService.GetByUserId(userId)!;
        if (store == null)
        {
            throw new NotFoundException($"user with id {userId} doesn't have a store");
        }

        if (product.StoreId != store.Id)
        {
            throw new ForbiddenResourceException("forbidden resource");
        }

        if (payload.Name != "") product.Name = payload.Name;
        if (payload.Description != "") product.Description = payload.Description;
        if (payload.Price != 0) product.Price = payload.Price;
        if (payload.Stock != 0) product.Stock = payload.Stock;
        if (payload.CategoryId != "") product.CategoryId = Guid.Parse(payload.CategoryId);
        if (photo != "") 
        {
            File.Delete(product.Photo);
            product.Photo = photo;
        }
        product.UpdatedAt = DateTime.Now;

        var newProduct = _repository.Update(product);
        await _persistence.SaveChangesAsync();

        return newProduct;
    }
}