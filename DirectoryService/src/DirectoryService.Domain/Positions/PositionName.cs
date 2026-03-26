using CSharpFunctionalExtensions;
using SharedKernel;

namespace DirectoryService.Domain.Positions;

public sealed record PositionName
{
    public const int NAME_MIN_LENGTH = 3;
    public const int NAME_MAX_LENGTH = 100;

    public string Value { get; }

    private PositionName(string value)
    {
        Value = value;
    }

    public static Result<PositionName, Errors> Create(string value)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(value))
        {
            errors.Add(GeneralErrors.ValueIsRequired("Имя позиции"));
        }

        if (value.Length is < NAME_MIN_LENGTH or > NAME_MAX_LENGTH)
        {
            errors.Add(GeneralErrors.ValueIsInvalid("Имя позиции"));
        }

        if (errors.Count > 0)
        {
            return new Errors(errors);
        }

        return new PositionName(value);
    }
}