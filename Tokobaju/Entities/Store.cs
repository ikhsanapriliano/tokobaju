using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tokobaju.Entities;

[Table("store")]
[Index(nameof(UserId), IsUnique = true)]
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
    public required DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    public virtual User? User { get; set; }
    public virtual ICollection<Product>? Products{ get; set; }
}