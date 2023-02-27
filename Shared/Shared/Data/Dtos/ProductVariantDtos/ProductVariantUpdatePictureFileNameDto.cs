using System.ComponentModel.DataAnnotations;

namespace Shared.Data.Dtos.ProductVariantDtos;

public record ProductVariantUpdatePictureFileNameDto
{
    [Required(ErrorMessage = "Picture file name is a required field.")]
    [MaxLength(128, ErrorMessage = "Maximum length for the Picture file name is 128 characters.")]
    public string PictureFileName { get; set; } = null!;
}