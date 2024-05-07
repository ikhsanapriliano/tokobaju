namespace Tokobaju.Dto;

public class ProductDto
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int Price { get; set;} = 0;
    public int Stock { get; set; } = 0;
    public string CategoryId { get; set; } = "";
}