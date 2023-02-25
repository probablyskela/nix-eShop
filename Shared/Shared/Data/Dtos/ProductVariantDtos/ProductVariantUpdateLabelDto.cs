using System.ComponentModel.DataAnnotations;

namespace Shared.Data.Dtos.ProductVariantDtos;

public record ProductVariantUpdateLabelDto
{
    [Required(ErrorMessage = "Label is a required field.")]
    [MaxLength(50, ErrorMessage = "Maximum length for the Label is 50 characters.")]
    public string Label { get; set; } = null!;
}