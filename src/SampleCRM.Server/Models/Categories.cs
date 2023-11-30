namespace SampleCRM.Web.Models;
#nullable enable

public partial class Categories
{
    [Key]
    public long CategoryID { get; set; }

    public string? Description { get; set; }

    public string Name { get; set; } = null!;

    public byte[]? Picture { get; set; }

    public byte[]? Thumbnail { get; set; }
}
