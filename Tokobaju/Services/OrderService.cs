using Tokobaju.Dto;
using Tokobaju.Entities;
using Tokobaju.Enums;
using Tokobaju.Exceptions;
using Tokobaju.Repositories;

namespace Tokobaju.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _oRepository;
    private readonly IRepository<OrderDetail> _odRepository;
    private readonly IPersistence _persistence;
    private readonly IProductService _productService;

    public OrderService(IRepository<Order> oRepository, IRepository<OrderDetail> odRepository, IPersistence persistence, IProductService productService)
    {
        _oRepository = oRepository;
        _odRepository = odRepository;
        _persistence = persistence;
        _productService = productService;
    }

    public async Task<Order> Create(string userId, CreateOrderDto payload)
    {
        await _persistence.BeginTransactionAsync();
        try
        {
            var totalPayment = 0;
            var products = new List<Product>();
            foreach (var item in payload.Products)
            {
                var product = await _productService.GetById(item.ProductId);
                totalPayment += product.Price * item.Quantity;
                products.Add(product);
            }

            var orderInput = new Order
            {
                OrderDate = DateTime.Now,
                UserId = Guid.Parse(userId),
                TotalPayment = totalPayment,
                Status = Enum.Parse<EOrderStatus>("Pending", true),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var order = await _oRepository.SaveAsync(orderInput);
            await _persistence.SaveChangesAsync();

            OrderProductDto[] orderProducts = new OrderProductDto[payload.Products.Count];
            for (var i = 0; i < orderProducts.Length; i++)
            {
                var orderDetail = new OrderDetail
                {
                    Quantity = orderProducts[i].Quantity,
                    TotalPrice = products[i].Price * orderProducts[i].Quantity,
                    ProductId = Guid.Parse(orderProducts[i].ProductId),
                    ShipperId = Guid.Parse(orderProducts[i].ShipperId),
                    OrderId = order.Id,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await _odRepository.SaveAsync(orderDetail);
                await _persistence.SaveChangesAsync();
            }
    
            await _persistence.CommitTransactionAsync();

            return order;
        }
        catch (Exception)
        {
            await _persistence.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<Order> GetById(string id)
    {
        var data = await _oRepository.FindByIdAsync(Guid.Parse(id));
        if (data == null)
        {
            throw new NotFoundException($"order with id {id} not found");
        }

        return data;
    }

    public async Task<List<Order>> GetByUserId(string userId)
    {
        var data = await _oRepository.FindAllAsync(data => data.UserId.Equals(userId));

        return data;
    }

    public async Task<Order> Update(string id)
    {
        var data = await GetById(id);
        data.Status = Enum.Parse<EOrderStatus>("Paid", true);
        data.UpdatedAt = DateTime.Now;

        var newData = _oRepository.Update(data);
        await _persistence.SaveChangesAsync();

        return newData;
    }
}