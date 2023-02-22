using System.ComponentModel.DataAnnotations;

namespace Shared.Data.Dtos.ProductDtos;

public class ProductForManipulationDto
{
    [Required(ErrorMessage = "Name is a required field.")]
    [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters.")]
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = "Description is a required field.")]
    [MaxLength(500, ErrorMessage = "Maximum length for the Description is 100 characters.")]
    public string Description { get; set; } = null!;
    
    [Required(ErrorMessage = "Picture id is a required field.")]
    public int PictureId { get; set; }
    
    [Required(ErrorMessage = "Category id is a required field.")]
    public int CategoryId { get; set; }
}