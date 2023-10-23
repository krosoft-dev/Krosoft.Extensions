using System.ComponentModel;

namespace Krosoft.Extensions.Samples.Library.Models;

public class Address
{
    public Address(string streetLine1, string streetLine2, string city, string zipCode)
    {
        StreetLine1 = streetLine1;
        StreetLine2 = streetLine2;
        City = city;
        ZipCode = zipCode;
    }

    [Description("Ligne1")]
    public string StreetLine1 { get; }

    public string StreetLine2 { get; }
    public string City { get; }
    public string ZipCode { get; }
}