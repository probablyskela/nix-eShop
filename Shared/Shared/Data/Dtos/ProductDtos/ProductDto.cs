namespace Shared.Data.Dtos.ProductDtos;

public record ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int AvailableStock { get; set; }
    public int PictureId { get; set; }
    public int CategoryId { get; set; }
    public IEnumerable<int> ConsumerIds { get; set; } = null!;
}