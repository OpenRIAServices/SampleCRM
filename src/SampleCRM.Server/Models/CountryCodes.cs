namespace SampleCRM.Web.Models;
#nullable enable

public partial class CountryCodes
{
    [Key]
    public string CountryCodeID { get; set; } = null!;

    public string Name { get; set; } = null!;
}
