namespace SampleCRM.Web.Models;
#nullable enable

public partial class PaymentTypes
{
    [Key]
    public long PaymentTypeID { get; set; }

    public string Name { get; set; } = null!;
}
