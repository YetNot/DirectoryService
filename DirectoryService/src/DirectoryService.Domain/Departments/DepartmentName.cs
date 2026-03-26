using CSharpFunctionalExtensions;
using SharedKernel;

namespace DirectoryService.Domain.Departments;

public sealed record DepartmentName
{
    public const int NAME_MIN_LENGTH = 3;

    public const int NAME_MAX_LENGTH = 150;

    public string Value { get; }

    private DepartmentName(string value)
    {
        Value = value;
    }

    public static Result<DepartmentName, Errors> Create(string value)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(GeneralErrors.ValueIsRequired("Название отделения"));
        }

        if (value.Length is < NAME_MIN_LENGTH or > NAME_MAX_LENGTH)
        {
            errors.Add(GeneralErrors.ValueIsInvalid("Название отделения"));
        }

        if (errors.Count > 0)
        {
            return new Errors(errors);
        }

        return new DepartmentName(value);
    }
}