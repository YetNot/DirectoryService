using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.ValueObjects;

public record Path
{
    public string Value { get; }

    private Path(string value)
    {
        Value = value;
    }

    public static Result<Path> Create(string value)
    {
        return string.IsNullOrWhiteSpace(value) ?
            Result.Failure<Path>("Денорм. путь невалидный") :
            new Path(value);
    }
}