using CSharpFunctionalExtensions;
using SharedKernel;

namespace DirectoryService.Domain.Positions;

public sealed record Description
{
    public const int DESCRIPTION_MAX_LENGTH = 1000;

    public string Value { get; }

    public Description(string value)
    {
        Value = value;
    }

    public static Result<Description, Error> Create(string value)
    {
        if (value.Length > DESCRIPTION_MAX_LENGTH)
        {
            return GeneralErrors.ValueIsInvalid("Позиция");
        }

        return new Description(value);
    }
}