using System.ComponentModel.DataAnnotations;

namespace Shared.Data.Dtos.ProductDtos;

public class ProductUpdateConsumerDto
{
    [Required(ErrorMessage = "Consumer id is a required field.")]
    [Range(1, int.MaxValue, ErrorMessage = "Consumer id is required and it can't be lower than 1.")]
    public int ConsumerId { get; set; }
}