namespace SampleCRM.Web.Models;
#nullable enable

public partial class Customers
{
    [Key]
    public long CustomerID { get; set; }

    public string AddressLine1 { get; set; } = null!;

    public string? AddressLine2 { get; set; }

    public string? BirthDate { get; set; }

    public long? ChildrenAtHome { get; set; }

    public string City { get; set; } = null!;

    public string CountryCode { get; set; } = null!;

    public string CreatedOn { get; set; } = null!;

    public string? Education { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? Gender { get; set; }

    public long? IsHouseOwner { get; set; }

    public string LastModifiedOn { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MaritalStatus { get; set; }

    public string? MiddleName { get; set; }

    public long? NumberCarsOwned { get; set; }

    public string? Occupation { get; set; }

    public string? Phone { get; set; }

    public byte[]? Picture { get; set; }

    public string PostalCode { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string? SearchTerms { get; set; }

    public string? Suffix { get; set; }

    public byte[]? Thumbnail { get; set; }

    public string? Title { get; set; }

    public long? TotalChildren { get; set; }

    public string? YearlyIncome { get; set; }

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
