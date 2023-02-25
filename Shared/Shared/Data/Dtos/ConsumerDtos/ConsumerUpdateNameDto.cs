using System.ComponentModel.DataAnnotations;

namespace Shared.Data.Dtos.ConsumerDtos;

public record ConsumerUpdateNameDto
{
    [Required(ErrorMessage = "Name is a required field.")]
    [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters.")]
    public string Name { get; set; } = null!;
}