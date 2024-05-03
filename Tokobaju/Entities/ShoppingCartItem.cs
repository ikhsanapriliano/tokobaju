using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tokobaju.Entities;

[Table("shopping_cart_items")]
public class ShoppingCartItem
{
    [Key, Column("id")]
    public Guid Id { get; set; }

    [Required, Column("quantity")]
    public required int Quantity { get; set; }

    [Required, Column("product_id")]
    public required Guid ProductId { get; set; }

    [Required, Column("cart_id")]
    public required Guid CartId { get; set; }

    [Required, Column("created_at")]
    public required DateTime CreatedAt { get; set; }

    [Required, Column("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    public virtual Product? Product{ get; set; }

    [ForeignKey("CartId")]
    public virtual ShoppingCart? ShoppingCart{ get; set; }
}