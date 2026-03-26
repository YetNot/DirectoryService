using System.Text.Json;
using SharedKernel;

namespace DirectoryService.Application.Exceptions;

public class FailureException : Exception
{
    protected FailureException(Error[] errors)
        : base(JsonSerializer.Serialize(errors))
    {
    }
}