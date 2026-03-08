using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.ValueObjects;

public record Identifier
{
    public const int MIN_LENGTH = 3;

    public const int MAX_LENGTH = 150;

    public string Value { get; }

    private Identifier(string value)
    {
        Value = value;
    }

    public static Result<Identifier> Create(string value)
    {
        if (!value.All(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
            || (string.IsNullOrWhiteSpace(value)
                || (value.Length <= MIN_LENGTH || value.Length >= MAX_LENGTH)))
        {
            return Result.Failure<Identifier>("Идентификатор невалидный");
        }

        return new Identifier(value);
    }
}