using System.ComponentModel.DataAnnotations;

namespace Shared.Data.Dtos.ProductVariantDtos;

public class ProductVariantForManipulationDto
{
    [Required(ErrorMessage = "Label is a required field.")]
    [MaxLength(50, ErrorMessage = "Maximum length for the Label is 50 characters.")]
    public string Label { get; set; } = null!;
    
    [Required(ErrorMessage = "Price is a required field.")]
    [Range(0, int.MaxValue, ErrorMessage = "Price is required and it can't be lower that 0.")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "Available stock is a required field.")]
    [Range(0, int.MaxValue, ErrorMessage = "Available stock is required and it can't be lower that 0.")]
    public int AvailableStock { get; set; }
    
    [Required(ErrorMessage = "Product id is a required field.")]
    public int ProductId { get; set; }
}