using System.ComponentModel.DataAnnotations;

namespace Shared.Data.Dtos.ProductDtos;

public record ProductUpdateCategoryDto
{
    [Required(ErrorMessage = "Category id is a required field.")]
    [Range(1, int.MaxValue, ErrorMessage = "Category id is required and it can't be lower than 1.")]
    public int CategoryId { get; set; }
}