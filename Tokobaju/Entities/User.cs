using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tokobaju.Entities;

[Table("users")]
[Index(nameof(Email), IsUnique = true)]
public class User 
{
    [Key, Column("id")]
    public Guid Id { get; set; }

    [Required, Column("name")]
    public required string Name { get; set; }

    [Required, Column("email")]
    public required string Email { get; set; }
    
    [Required, Column("password")]
    public string? Password { get; set; }
    
    [Required, Column("role")]
    public required string Role { get; set; }

    [Required,Column("photo")]    
    public required string Photo { get; set; }

    [Required, Column("created_at")]
    public required DateTime CreatedAt { get; set; }

    [Required, Column("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    public virtual ICollection<Store>? Stores{ get; set; }
}