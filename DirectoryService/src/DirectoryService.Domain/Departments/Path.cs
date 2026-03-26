using CSharpFunctionalExtensions;
using SharedKernel;

namespace DirectoryService.Domain.Departments;

public sealed record Path
{
    private const char PATH_SEPARATOR = '/';

    public string Value { get; }

    private Path(string value)
    {
        Value = value;
    }

    public static Path CreateParent(Identifier identifier)
    {
        return new Path(identifier.Value);
    }

    public Path CreateChild(Identifier identifier)
    {
        return new Path(Value + PATH_SEPARATOR + identifier.Value);
    }
}