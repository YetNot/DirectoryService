using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using SharedKernel;

namespace DirectoryService.Domain.Departments;

public sealed record Identifier
{
    private static readonly Regex _identifierRegex = new("^[a-zA-Z]+$", RegexOptions.Compiled);

    public const int IDENTIFIER_MIN_LENGTH = 3;
    public const int IDENTIFIER_MAX_LENGTH = 150;

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

        if (!_identifierRegex.IsMatch(value))
        {
            errors.Add(GeneralErrors.ValueIsInvalid("Идентификатор"));
        }

        if (value.Length is < IDENTIFIER_MIN_LENGTH or > IDENTIFIER_MAX_LENGTH)
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