using CSharpFunctionalExtensions;

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

    public static Result<NameDepartment> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)
            || (value.Length <= MIN_LENGTH || value.Length >= MAX_LENGTH))
        {
            return Result.Failure<NameDepartment>("Имя отделения невалидное");
        }

        return new NameDepartment(value);
    }
}