using System.ComponentModel.DataAnnotations;

namespace Shared.Data.Dtos.ProductDtos;

public record ProductUpdateNameDto
{
    [Required(ErrorMessage = "Name is a required field.")]
    [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters.")]
    public string Name { get; set; } = null!;
}