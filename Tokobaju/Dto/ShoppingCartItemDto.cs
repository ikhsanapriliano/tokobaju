using System.ComponentModel.DataAnnotations;

namespace Tokobaju.Dto;

public class ShoppingCartItemDto 
{
    public string ProductId { get; set; } = "";
    
    [Required(ErrorMessage = "quantity required")]
    public int Quantity { get; set; } = 0;
}