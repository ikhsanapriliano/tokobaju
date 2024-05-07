using Tokobaju.Entities;

namespace Tokobaju.Services;

public interface IShoppingCartService
{
    Task<ShoppingCart> Create(string userId);
    Task<ShoppingCart> GetByUserId(string userId);
    Task<bool> Delete(string userId);
}