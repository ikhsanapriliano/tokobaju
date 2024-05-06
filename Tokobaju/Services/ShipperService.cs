using Microsoft.AspNetCore.Http.HttpResults;
using Tokobaju.Dto;
using Tokobaju.Entities;
using Tokobaju.Exceptions;
using Tokobaju.Repositories;

namespace Tokobaju.Services;

public class ShipperService : IShipperService
{
    private readonly IRepository<Shipper> _repository;
    private readonly IPersistence _persistence;

    public ShipperService(IRepository<Shipper> repository, IPersistence persistence)
    {
        _repository = repository;
        _persistence = persistence;
    }

    public async Task<Shipper> Create(ShipperDto payload)
    {
        var input = new Shipper
        {
            Name = payload.Name,
            Service = payload.Service,
            ShippingFee = payload.ShippingFee,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var data = await _repository.SaveAsync(input);
        await _persistence.SaveChangesAsync();

        return data;
    }

    public async Task Delete(string id)
    {
        var shipper = await GetById(id);
        _repository.Delete(shipper);
        await _persistence.SaveChangesAsync();
    }

    public async Task<List<Shipper>> GetAll()
    {
        var data = await _repository.FindAllAsync();

        return data;
    }

    public async Task<Shipper> GetById(string id)
    {
        var data = await _repository.FindByIdAsync(Guid.Parse(id));
        if (data == null)
        {
            throw new NotFoundException($"shipper with id {id} not found");
        }

        return data;
    }

    public async Task<Shipper> Update(string id, ShipperDto payload)
    {
        var shipper = await GetById(id);
        if (payload.Name != "") shipper.Name = payload.Name;
        if (payload.Service != "") shipper.Service = payload.Service;
        if (payload.ShippingFee != 0) shipper.ShippingFee = payload.ShippingFee;

        var newShipper = _repository.Update(shipper);
        await _persistence.SaveChangesAsync();

        return newShipper;
    }
}