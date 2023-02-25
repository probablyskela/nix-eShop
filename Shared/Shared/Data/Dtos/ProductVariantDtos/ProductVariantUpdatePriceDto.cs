using System.ComponentModel.DataAnnotations;

namespace Shared.Data.Dtos.ProductVariantDtos;

public record ProductVariantUpdatePriceDto
{
    [Required(ErrorMessage = "Price is a required field.")]
    [Range(0, int.MaxValue, ErrorMessage = "Price is required and it can't be lower that 0.")]
    public decimal Price { get; set; }
}