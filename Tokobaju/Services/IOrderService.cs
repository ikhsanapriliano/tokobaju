using Tokobaju.Dto;
using Tokobaju.Entities;

namespace Tokobaju.Services;

public interface IOrderService
{
    Task<Order> Create(string userId, CreateOrderDto payload);
    Task<Order> GetById(string id);
    Task<List<Order>> GetByUserId(string userId);
    Task<Order> Update(string id);
}