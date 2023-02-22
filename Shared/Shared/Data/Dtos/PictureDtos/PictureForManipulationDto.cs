using System.ComponentModel.DataAnnotations;

namespace Shared.Data.Dtos.PictureDtos;

public class PictureForManipulationDto
{
    [Required(ErrorMessage = "Picture file name is a required field.")]
    [MaxLength(100, ErrorMessage = "Maximum length for the Picture file name is 100 characters.")]
    public string PictureFileName { get; set; } = null!;
}