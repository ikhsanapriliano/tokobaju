using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tokobaju.Entities;

[Table("shippers")]
[Index(nameof(Name), IsUnique = true)]
public class Shipper
{
    [Key, Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public required string Name { get; set; }
    
    [Column("service")]
    public required string Service { get; set; }

    [Column("shipping_fee")]
    public required int ShippingFee { get; set; }

    [Column("created_at")]
    public required DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public required DateTime UpdatedAt { get; set; }
}