using System.ComponentModel.DataAnnotations;
using Shared.Data.Dtos.ProductVariantDtos;

namespace Shared.Data.Dtos.ProductDtos;

public abstract record ProductForManipulationDto
{
    [Required(ErrorMessage = "Name is a required field.")]
    [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Description is a required field.")]
    [MaxLength(500, ErrorMessage = "Maximum length for the Description is 500 characters.")]
    public string Description { get; set; } = null!;
    
    [Required(ErrorMessage = "Picture file name is a required field.")]
    [MaxLength(128, ErrorMessage = "Maximum length for the picture file name is 128 characters.")]
    public string PictureFileName { get; set; } = null!;

    [Required(ErrorMessage = "Category id is a required field.")]
    [Range(1, int.MaxValue, ErrorMessage = "Category id is required and it can't be lower than 1.")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Consumer ids is a required field.")]
    public IEnumerable<int> ConsumerIds { get; set; } = null!;
}