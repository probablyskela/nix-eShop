namespace Shared.Data.Entities;

public class ProductVariant
{
    public int Id { get; set; }
    public string Label { get; set; } = null!;
    public decimal Price { get; set; }
    public int AvailableStock { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}