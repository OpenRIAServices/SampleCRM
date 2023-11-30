namespace SampleCRM.Web.Models;
#nullable enable

public partial class Orders
{
    [Key]
    public long OrderID { get; set; }

    public long CustomerID { get; set; }

    public string? DeliveredDate { get; set; }

    public string LastModifiedOn { get; set; } = null!;

    public string OrderDate { get; set; } = null!;

    public long? PaymentType { get; set; }

    public string? SearchTerms { get; set; }

    public string? ShipAddress { get; set; }

    public string? ShipCity { get; set; }

    public string? ShipCountryCode { get; set; }

    public string? ShipPhone { get; set; }

    public string? ShipPostalCode { get; set; }

    public string? ShipRegion { get; set; }

    public long? ShipVia { get; set; }

    public string? ShippedDate { get; set; }

    public long Status { get; set; }

    public string? TrackingNumber { get; set; }

    public virtual Customers Customer { get; set; } = null!;

    public virtual ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
}
