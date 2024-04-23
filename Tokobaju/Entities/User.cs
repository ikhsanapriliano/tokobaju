using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tokobaju.Entities;

[Table(name: "users")]
[Index(nameof(Email), IsUnique = true)]
public class User {
    [Key, Column(name: "id")]
    public Guid Id { get; set; }

    [Required, Column(name: "name", TypeName = "NVarchar(100)")]
    public required string Name { get; set; }

    [Required, Column(name: "email", TypeName = "NVarchar(100)")]
    public required string Email { get; set; }
    
    [Required, Column(name: "password", TypeName = "NVarchar(100)")]
    public string? Password { get; set; }
    
    [Required, Column(name: "role", TypeName = "NVarchar(100)")]
    public required string Role { get; set; }

    [Required,Column(name: "photo", TypeName = "NVarchar(100)")]    
    public required string Photo { get; set; }
}