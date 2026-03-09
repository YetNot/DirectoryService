using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.ValueObjects;

public record Address
{
    public string Country { get; }

    public string City { get; }

    public string Street { get; }

    public string HouseNumber { get; }

    private Address(
        string country,
        string city,
        string street,
        string houseNumber)
    {
        Country = country;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
    }

    public static Result<Address> Create(
        string country,
        string city,
        string street,
        string houseNumber)
    {
        return new Address(country, city, street, houseNumber);
    }
}