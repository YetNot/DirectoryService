using System.Text.Json;
using SharedKernel;

namespace DirectoryService.Application.Exceptions;

public class ConflictException : Exception
{
    protected ConflictException(Error[] errors)
        : base(JsonSerializer.Serialize(errors))
    {
    }
}