using System.ComponentModel.DataAnnotations;

namespace Tokobaju.Dto;

public class ProductCategoryDto
{
    [Required(ErrorMessage = "name required")]
    public string Name { get; set; } = "";
}