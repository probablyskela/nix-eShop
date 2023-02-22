namespace Shared.Data.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int PictureId { get; set; }
    public Picture Picture { get; set; } = null!;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public List<Consumer> Consumers { get; set; } = null!;
    public List<ProductVariant> ProductVariants { get; set; } = null!;
}