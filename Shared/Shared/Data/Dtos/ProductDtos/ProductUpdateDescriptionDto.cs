using System.ComponentModel.DataAnnotations;

namespace Shared.Data.Dtos.ProductDtos;

public record ProductUpdateDescriptionDto
{
    [Required(ErrorMessage = "Description is a required field.")]
    [MaxLength(500, ErrorMessage = "Maximum length for the Description is 500 characters.")]
    public string Description { get; set; } = null!;
}