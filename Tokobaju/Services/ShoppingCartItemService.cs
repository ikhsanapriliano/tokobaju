using Tokobaju.Dto;
using Tokobaju.Entities;
using Tokobaju.Exceptions;
using Tokobaju.Repositories;

namespace Tokobaju.Services;

public class ShoppingCartItemService : IShoppingCartItemService
{
    private readonly IRepository<ShoppingCartItem> _repository;
    private readonly IPersistence _persistence;
    private readonly IShoppingCartService _shoppingCartService;
 
    public ShoppingCartItemService(IRepository<ShoppingCartItem> repository, IPersistence persistence, IShoppingCartService shoppingCartService)
    {
        _repository = repository;
        _persistence = persistence;
        _shoppingCartService = shoppingCartService;
    }

    public async Task<ShoppingCartItem> Create(string userId, ShoppingCartItemDto payload)
    {
        if (payload.Quantity <= 0)
        {
            throw new BadHttpRequestException("quantity min 1");
        }

        var shoppingCart = await _shoppingCartService.GetByUserId(userId);
        var input = new ShoppingCartItem
        {
            ProductId = Guid.Parse(payload.ProductId),
            Quantity = payload.Quantity,
            CartId = shoppingCart.Id,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var data = await _repository.SaveAsync(input);
        await _persistence.SaveChangesAsync();

        return data; 
    }

    public async Task Delete(string userId, string cartItemId)
    {
        var shoppingCart = await _shoppingCartService.GetByUserId(userId);
        var data = await GetById(cartItemId);
        if (data.CartId != shoppingCart.Id)
        {
            throw new ForbiddenResourceException("forbidden resource");
        }

        _repository.Delete(data);
        await _persistence.SaveChangesAsync();
    }

    public async Task<ShoppingCartItem> GetById(string id)
    {
        var data = await _repository.FindByIdAsync(Guid.Parse(id));
        if (data == null)
        {
            throw new NotFoundException($"data with id {id} not found");
        }

        return data;
    }

    public async Task<List<ShoppingCartItem>> GetByUserId(string userId)
    {
        var cart = await _shoppingCartService.GetByUserId(userId);
        var data = await _repository.FindAllAsync(data => data.CartId.Equals(cart.Id));
        if (data.Count <= 0)
        {
            throw new NotFoundException($"user with id {userId} doesn't have any shopping cart items");
        }

        return data;
    }

    public async Task<ShoppingCartItem> Update(string userId, string cartItemId, int quantity)
    {
        var shoppingCart = await _shoppingCartService.GetByUserId(userId);
        var data = await GetById(cartItemId);
        if (data.CartId != shoppingCart.Id)
        {
            throw new ForbiddenResourceException("forbidden resource");
        }

        data.Quantity = quantity;
        data.UpdatedAt = DateTime.Now;

        var newData = _repository.Update(data);
        await _persistence.SaveChangesAsync();

        return newData;
    }
}