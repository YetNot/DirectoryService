using CSharpFunctionalExtensions;
using SharedKernel;

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

    public static Result<NameLocation, Errors> Create(string value)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(GeneralErrors.ValueIsRequired("Название локации"));
        }

        if (value.Length <= MIN_LENGTH || value.Length >= MAX_LENGTH)
        {
            errors.Add(GeneralErrors.ValueIsInvalid("Название локации"));
        }

        if (errors.Count > 0)
        {
            return new Errors(errors);
        }

        return new NameLocation(value);
    }
}