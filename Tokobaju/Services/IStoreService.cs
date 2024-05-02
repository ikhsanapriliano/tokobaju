using Tokobaju.Dto;
using Tokobaju.Entities;

namespace Tokobaju.Services;

public interface IStoreService
{
    Task<Store> Create(string userId, string photo, StoreDto payload);
    Task<Store>? GetByUserId(string userId);
    Task<List<Store>> GetAll();
    Task<Store> Update(string userId, string photo, StoreDto payload);
    Task<bool> Delete(string userId);
}