using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.ValueObjects;

public record Address
{
    public string Value { get; }

    private Address(string value)
    {
        Value = value;
    }

    public static Result<Address> Create(string value)
    {
        return string.IsNullOrWhiteSpace(value) ?
            Result.Failure<Address>("Адрес невалидный") :
            new Address(value);
    }
}