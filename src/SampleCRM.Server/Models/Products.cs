namespace SampleCRM.Web.Models;
#nullable enable
public partial class Products
{
    [Key]
    public string ProductID { get; set; } = null!;

    public long CategoryID { get; set; }

    public string? Color { get; set; }

    public string CreatedOn { get; set; } = null!;

    public string DealerPrice { get; set; } = null!;

    public string? Description { get; set; }

    public string Discount { get; set; } = null!;

    public string? DiscountEndDate { get; set; }

    public string? DiscountStartDate { get; set; }

    public string LastModifiedOn { get; set; } = null!;

    public string ListPrice { get; set; } = null!;

    public string Name { get; set; } = null!;

    public byte[]? Picture { get; set; }

    public long SafetyStockLevel { get; set; }

    public string? SearchTerms { get; set; }

    public string? Size { get; set; }

    public long StockUnits { get; set; }

    public long TaxType { get; set; }

    public byte[]? Thumbnail { get; set; }

    public virtual ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
}
