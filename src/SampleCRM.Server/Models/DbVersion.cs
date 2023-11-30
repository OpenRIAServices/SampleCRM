namespace SampleCRM.Web.Models;
#nullable enable

public partial class DbVersion
{
    [Key]
    public string Version { get; set; } = null!;
}
