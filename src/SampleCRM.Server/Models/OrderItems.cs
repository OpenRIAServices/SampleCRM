namespace SampleCRM.Web.Models;
#nullable enable
public partial class OrderItems
{
    [Key]
    public long OrderID { get; set; }
    [Key]
    public long OrderLine { get; set; }

    public string Discount { get; set; } = null!;

    public string ProductID { get; set; } = null!;

    public long Quantity { get; set; }

    public long TaxType { get; set; }

    public string UnitPrice { get; set; } = null!;

    public virtual Orders Order { get; set; } = null!;

    public virtual Products Product { get; set; } = null!;
}
