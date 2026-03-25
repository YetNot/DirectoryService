using CSharpFunctionalExtensions;
using SharedKernel;

namespace DirectoryService.Domain.ValueObjects;

public record NameDepartment
{
    public const int MIN_LENGTH = 3;

    public const int MAX_LENGTH = 150;

    public string Value { get; }

    private NameDepartment(string value)
    {
        Value = value;
    }

    public static Result<NameDepartment, Errors> Create(string value)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(GeneralErrors.ValueIsRequired("Название отделения"));
        }

        if (value.Length <= MIN_LENGTH || value.Length >= MAX_LENGTH)
        {
            errors.Add(GeneralErrors.ValueIsInvalid("Название отделения"));
        }

        if (errors.Count > 0)
        {
            return new Errors(errors);
        }

        return new NameDepartment(value);
    }
}