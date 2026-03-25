using CSharpFunctionalExtensions;
using SharedKernel;

namespace DirectoryService.Domain.ValueObjects;

public record Path
{
    public string Value { get; }

    private Path(string value)
    {
        Value = value;
    }

    public static Result<Path, Error> Create(string value)
    {
        return string.IsNullOrWhiteSpace(value) ?
            GeneralErrors.ValueIsRequired("Путь") :
            new Path(value);
    }
}