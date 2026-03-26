using CSharpFunctionalExtensions;
using SharedKernel;

namespace DirectoryService.Domain.Locations;

public sealed record TimeZone
{
    public string Value { get; }

    private TimeZone(string value)
    {
        Value = value;
    }

    public static Result<TimeZone, Errors> Create(string value)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(GeneralErrors.ValueIsRequired("Часовой пояс"));
        }

        if (!TimeZoneInfo.GetSystemTimeZones().Any(z => z.Id != value))
        {
            errors.Add(GeneralErrors.ValueIsInvalid("Часовой пояс"));
        }

        if (errors.Count > 0)
        {
            return new Errors(errors);
        }

        return new TimeZone(value);
    }
}