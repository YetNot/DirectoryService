using CSharpFunctionalExtensions;
using SharedKernel;

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

    public static Result<Identifier, Errors> Create(string value)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(GeneralErrors.ValueIsRequired("Идентификатор"));
        }

        if (!value.All(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')))
        {
            errors.Add(GeneralErrors.ValueIsInvalid("Идентификатор"));
        }

        if (value.Length <= MIN_LENGTH || value.Length >= MAX_LENGTH)
        {
            errors.Add(GeneralErrors.ValueIsInvalid("Идентификатор"));
        }

        if (errors.Count > 0)
        {
            return new Errors(errors);
        }

        return new Identifier(value);
    }
}