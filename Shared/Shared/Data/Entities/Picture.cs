namespace Shared.Data.Entities;

public class Picture
{
    public int Id { get; set; }
    public string PictureFileName { get; set; } = null!;
    public List<ProductVariant> ProductVariants { get; set; } = null!;
}