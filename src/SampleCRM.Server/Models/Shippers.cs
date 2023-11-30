namespace SampleCRM.Web.Models;
#nullable enable

public partial class Shippers
{
    [Key]
    public long ShipperID { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }
}
