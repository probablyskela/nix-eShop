using System.ComponentModel.DataAnnotations;

namespace Shared.Data.Dtos.ProductVariantDtos;

public record ProductVariantUpdateAvailableStockDto
{
    [Required(ErrorMessage = "Available stock is a required field.")]
    [Range(0, int.MaxValue, ErrorMessage = "Available stock is required and it can't be lower than 0.")]
    public int AvailableStock { get; set; }
}