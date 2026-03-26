using CSharpFunctionalExtensions;
using SharedKernel;

namespace DirectoryService.Domain.Locations;

public sealed record LocationName
{
    public const int LOCATION_MIN_LENGTH = 3;

    public const int LOCATION_MAX_LENGTH = 120;

    public string Value { get; }

    private LocationName(string value)
    {
        Value = value;
    }

    public static Result<LocationName, Errors> Create(string value)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(GeneralErrors.ValueIsRequired("Название локации"));
        }

        if (value.Length is < LOCATION_MIN_LENGTH or > LOCATION_MAX_LENGTH)
        {
            errors.Add(GeneralErrors.ValueIsInvalid("Название локации"));
        }

        if (errors.Count > 0)
        {
            return new Errors(errors);
        }

        return new LocationName(value);
    }
}