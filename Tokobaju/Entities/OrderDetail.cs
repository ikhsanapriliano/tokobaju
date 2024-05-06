using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tokobaju.Entities;

[Table("order_details")]
public class OrderDetail
{
    [Key, Column("id")]
    public Guid Id { get; set; }

    [Column("quantity")]
    public required int Quantity { get; set; }

    [Column("total_price")]
    public required int TotalPrice { get; set; }

    [Column("product_id")]
    public required Guid ProductId { get; set; }

    [Column("shipper_id")]
    public required Guid ShipperId { get; set; }

    [Column("order_id")]
    public required Guid OrderId { get; set; }

    [Column("created_at")]
    public required DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    public virtual Product? Product{ get; set; }
    public virtual Shipper? Shipper{ get; set; }
    public virtual Order? Order{ get; set; }
}