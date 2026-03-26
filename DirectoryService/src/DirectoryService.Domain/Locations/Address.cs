using CSharpFunctionalExtensions;
using SharedKernel;

namespace DirectoryService.Domain.Locations;

public sealed record Address
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

    public static Result<Address, Errors> Create(
        string country,
        string city,
        string street,
        string houseNumber)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(country))
        {
            errors.Add(GeneralErrors.ValueIsRequired("Страна"));
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            errors.Add(GeneralErrors.ValueIsRequired("Город"));
        }

        if (string.IsNullOrWhiteSpace(street))
        {
            errors.Add(GeneralErrors.ValueIsRequired("Улица"));
        }

        if (string.IsNullOrWhiteSpace(houseNumber))
        {
            errors.Add(GeneralErrors.ValueIsRequired("Номер дома"));
        }

        if (errors.Count > 0)
        {
            return new Errors(errors);
        }

        return new Address(country, city, street, houseNumber);
    }
}