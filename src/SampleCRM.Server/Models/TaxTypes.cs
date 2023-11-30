namespace SampleCRM.Web.Models;
#nullable enable

public partial class TaxTypes
{
    [Key]
    public long TaxTypeID { get; set; }

    public string Name { get; set; } = null!;

    public string Rate { get; set; } = null!;
}
