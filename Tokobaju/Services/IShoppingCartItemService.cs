using Tokobaju.Dto;
using Tokobaju.Entities;

namespace Tokobaju.Services;

public interface IShoppingCartItemService
{
    Task<ShoppingCartItem> Create(string userId, ShoppingCartItemDto payload);
    Task<ShoppingCartItem> GetById(string id);
    Task<List<ShoppingCartItem>> GetByUserId(string userId);
    Task<ShoppingCartItem> Update(string userId, string cartItemId, int quantity);
    Task Delete(string userId, string cartItemId);
}