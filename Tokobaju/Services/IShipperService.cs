using Tokobaju.Dto;
using Tokobaju.Entities;

namespace Tokobaju.Services;

public interface IShipperService
{
    Task<Shipper> Create(ShipperDto payload);
    Task<Shipper> GetById(string id);
    Task<List<Shipper>> GetAll();
    Task<Shipper> Update(string id, ShipperDto payload);
    Task Delete(string id);
}