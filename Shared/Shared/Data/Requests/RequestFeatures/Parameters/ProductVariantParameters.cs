namespace Shared.Data.Requests.RequestFeatures.Parameters;

public class ProductVariantParameters : RequestParameters
{
    public uint MinPrice { get; set; }
    public uint MaxPrice { get; set; } = uint.MaxValue;
    public bool ValidPriceRange => MinPrice < MaxPrice;
}