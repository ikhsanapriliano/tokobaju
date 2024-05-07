using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tokobaju.Entities;

[Table("shopping_carts")]
[Index(nameof(UserId), IsUnique = true)]
public class ShoppingCart 
{
    [Key, Column("id")]
    public Guid Id { get; set; }
    
    [Required, Column("user_id")]
    public required Guid UserId { get; set; }

    [Required, Column("created_at")]
    public required DateTime CreatedAt { get; set; }

    [Required, Column("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    public virtual User? User{ get; set; }
    public virtual ICollection<ShoppingCartItem>? CartItems{ get; set; }
}