namespace Shared.Data.Entities;

public class ProductVariantPicture
{
    public int ProductVariantId { get; set; }
    public ProductVariant ProductVariant { get; set; } = null!;
    public string PictureFileName { get; set; } = null!;
}