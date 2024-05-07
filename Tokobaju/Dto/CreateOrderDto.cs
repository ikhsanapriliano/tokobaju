using System.ComponentModel.DataAnnotations;

namespace Tokobaju.Dto;

public class CreateOrderDto
{
    [Required(ErrorMessage = "min 1 product required")]
    public required ICollection<OrderProductDto> Products;
}

public class OrderProductDto
{
    [Required(ErrorMessage = "productId required")]
    public string ProductId { get; set; } = "";

    [Required(ErrorMessage = "shipperId requried")]
    public string ShipperId { get; set; } = "";

    [Required(ErrorMessage = "quantity requried")]
    public int Quantity { get; set; } = 0;
}