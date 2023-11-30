namespace SampleCRM.Web.Models;
#nullable enable

public partial class OrderStatus
{
    [Key]
    public long Status { get; set; }

    public string Name { get; set; } = null!;
}
