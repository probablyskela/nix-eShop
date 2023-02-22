﻿namespace Shared.Data.Requests.RequestFeatures.Parameters;

public class ProductParameters : RequestParameters
{
    public uint MinPrice { get; set; }
    public uint MaxPrice { get; set; } = uint.MaxValue;
    public string? SearchTerm { get; set; }
    public IEnumerable<string>? Consumers { get; set; }
    public bool ValidPriceRange => MinPrice < MaxPrice;
}