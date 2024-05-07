using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tokobaju.Enums;

namespace Tokobaju.Entities;

[Table("orders")]
public class Order
{
    [Key, Column("id")]
    public Guid Id { get; set; }

    [Column("order_date")]
    public required DateTime OrderDate { get; set; }

    [Column("user_id")]
    public required Guid UserId { get; set; }

    [Column("total_payment")]
    public required int TotalPayment { get; set; }

    [Column("status")]
    public required EOrderStatus Status { get; set; }

    [Column("created_at")]
    public required DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    public virtual User? User{ get; set; }
    public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
}