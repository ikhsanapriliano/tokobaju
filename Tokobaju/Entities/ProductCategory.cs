using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tokobaju.Entities;

[Table("product_categories")]
[Index(nameof(Name), IsUnique = true)]
public class ProductCategory 
{
    [Key, Column("id")]
    public Guid Id { get; set;}

    [Column("name")]
    public required string Name { get; set;}

    [Column("created_at")]
    public required DateTime CreatedAt { get; set;}

    [Column("updated_at")]
    public required DateTime UpdatedAt { get; set;}

    public virtual ICollection<Product>? Products { get; set; }
}