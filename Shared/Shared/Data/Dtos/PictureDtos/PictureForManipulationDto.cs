using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Shared.Data.Dtos.Attributes;

namespace Shared.Data.Dtos.PictureDtos;

public record PictureForManipulationDto
{
    [Required(ErrorMessage = "Picture file is a required field.")]
    [MaxFileSize(2 * 1024 * 1024)]
    [AllowedExtensions(new[] { ".png" })]
    public IFormFile PictureFile { get; set; } = null!;
}