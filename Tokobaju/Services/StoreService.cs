using Tokobaju.Dto;
using Tokobaju.Entities;
using Tokobaju.Exceptions;
using Tokobaju.Repositories;

namespace Tokobaju.Services;

public class StoreService : IStoreService
{
    private readonly IRepository<Store> _repository;
    private readonly IPersistence _persistence;

    public StoreService(IRepository<Store> repository, IPersistence persistence)
    {
        _repository = repository;
        _persistence = persistence;
    }

    public async Task<Store> Create(string userId, string photo, StoreDto payload)
    {
        var store = await GetByUserId(userId)!;
        if (store != null)
        {
            throw new NotFoundException($"user with id {userId} doesnt have store");
        }

        var input = new Store
        {
            Name = payload.Name!,
            Description = payload.Description!,
            Photo = photo,
            UserId = store!.UserId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var newStore = await _repository.SaveAsync(input);
        await _persistence.SaveChangesAsync();

        return newStore;
    }

    public async Task<bool> Delete(string userId)
    {
        var store = await GetByUserId(userId)!;
        if (store == null)
        {
            throw new NotFoundException($"user with id {userId} doesnt have store");
        }

        _repository.Delete(store);
        await _persistence.SaveChangesAsync();

        return true;
    }

    public async Task<List<Store>> GetAll()
    {
        var stores = await _repository.FindAllAsync();

        return stores;
    }

    public async Task<Store>? GetByUserId(string userId)
    {
        var store = await _repository.FindAsync(store => store.UserId.Equals(userId));

        return store!;
    }

    public async Task<Store> Update(string userId, string photo, StoreDto payload)
    {
        var store = await GetByUserId(userId)!;
        if (store == null)
        {
            throw new NotFoundException($"user with id {userId} doesnt have store");
        }

        if (payload.Name != "") store.Name = payload.Name;
        if (payload.Description != "") store.Description = payload.Description;
        if (photo != "") 
        {
            if (!store.Photo.Contains("unknown")) File.Delete(store.Photo);
            store.Photo = photo;
        }
        store.UpdatedAt = DateTime.Now;

        var newStore = _repository.Update(store);
        await _persistence.SaveChangesAsync();

        return newStore;
    }
}