using Tokobaju.Entities;
using Tokobaju.Repositories;

namespace Tokobaju.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IRepository<ShoppingCart> _repository;
    private readonly IPersistence _persistence;

    public ShoppingCartService(IRepository<ShoppingCart> repository, IPersistence persistence)
    {
        _repository = repository;
        _persistence = persistence;
    }

    public async Task<ShoppingCart> Create(string userId)
    {
        var input = new ShoppingCart
        {
            UserId = Guid.Parse(userId),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
        var data = await _repository.SaveAsync(input);
        await _persistence.SaveChangesAsync();

        return data;
    }

    public async Task<bool> Delete(string userId)
    {
        var data = await _repository.FindAsync(data => data.UserId.Equals(userId));
        _repository.Delete(data!);
        await _persistence.SaveChangesAsync();

        return true;
    }
}