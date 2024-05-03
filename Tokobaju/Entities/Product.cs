using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tokobaju.Entities;

[Table("products")]
public class Product 
{
    [Key, Column("id")]
    public Guid Id { get; set;}

    [Column("name")]
    public required string Name { get; set;}

    [Column("description")]
    public required string Description { get; set;}

    [Column("photo")]
    public required string Photo { get; set;}

    [Column("price")]
    public required int Price { get; set;}

    [Column("stock")]
    public required int Stock { get; set;}

    [Column("category_id")]
    public required Guid CategoryId { get; set;}

    [Column("store_id")]
    public required Guid StoreId { get; set;}

    [Column("created_at")]
    public required DateTime CreatedAt {get; set;}

    [Column("updated_at")]
    public required DateTime UpdatedAt {get; set;}

    [ForeignKey("CategoryId")]
    public virtual ProductCategory? ProductCategory {get; set;}
    public virtual Store? Store {get; set;}
}