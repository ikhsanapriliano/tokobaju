using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tokobaju.Entities;

[Table("store")]
public class Store
{
    [Key, Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public required string Name { get; set; }

    [Column("description")]
    public required string Description { get; set; }

    [Column("photo")]
    public required string Photo { get; set; }

    [Column("user_id")]
    public required Guid UserId { get; set; }

    [Column("created_at")]
    public required string CreatedAt { get; set; }

    [Column("updated_at")]
    public required string UpdatedAt { get; set; }

    public virtual User? User { get; set; }
    public virtual ICollection<Product>? Products{ get; set; }
}