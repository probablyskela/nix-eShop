using System.ComponentModel.DataAnnotations;

namespace Shared.Data.Dtos.ProductDtos;

public record ProductUpdatePictureFileNameDto
{
    [Required(ErrorMessage = "Picture file name is a required field.")]
    [MaxLength(128, ErrorMessage = "Maximum length for the picture file name is 128 characters.")]
    public string PictureFileName { get; set; } = null!;
}