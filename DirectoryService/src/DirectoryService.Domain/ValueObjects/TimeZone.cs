using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.ValueObjects;

public record TimeZone
{
    public string Value { get; }

    private TimeZone(string value)
    {
        Value = value;
    }

    public static Result<TimeZone> Create(string value)
    {
        return string.IsNullOrWhiteSpace(value) ?
            Result.Failure<TimeZone>("Временю зона невалидная") :
            new TimeZone(value);
    }
}