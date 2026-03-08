using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.ValueObjects;

public record NameLocation
{
    public const int MIN_LENGTH = 3;

    public const int MAX_LENGTH = 120;

    public string Value { get; }

    private NameLocation(string value)
    {
        Value = value;
    }

    public static Result<NameLocation> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)
            || (value.Length <= MIN_LENGTH || value.Length >= MAX_LENGTH))
        {
            return Result.Failure<NameLocation>("Имя локации невалидное");
        }

        return new NameLocation(value);
    }
}