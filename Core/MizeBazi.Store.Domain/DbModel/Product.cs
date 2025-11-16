using MizeBazi.Store.Common.Shared;
namespace MizeBazi.Store.Domain;

public class DbProduct : SoftDeleteEntity
{
    public string Name { get; set; }
    public string Images { get; set; } // JSON array of image URLs

    public decimal Price { get; set; }

    public decimal CompareAtPrice { get; set; }

    public string Description { get; set; }

    public int Quantity { get; set; } //موجودی انبار

    public string SKU { get; set; } // کد کالا

    public bool IsPublished { get; set; } //وضعیت انتشار

    // Brand - یک برند
    public long BrandId { get; set; }

}