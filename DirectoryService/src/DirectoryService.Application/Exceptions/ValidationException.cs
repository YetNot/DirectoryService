using System.Text.Json;
using SharedKernel;

namespace DirectoryService.Application.Exceptions;

public class ValidationException : Exception
{
    protected ValidationException(Error[] errors)
        : base(JsonSerializer.Serialize(errors))
    {
    }
}