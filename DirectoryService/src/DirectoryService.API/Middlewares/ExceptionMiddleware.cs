using System.Text.Json;
using DirectoryService.Application.Exceptions;
using DirectoryService.Presentation.Envelopes;
using SharedKernel;

namespace DirectoryService.Presentation.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Exception was thrown in DirectoryService");

        (int statusCode, Error[] errors) = exception switch
        {
            BadRequestException => (
                StatusCodes.Status400BadRequest, JsonSerializer.Deserialize<Error[]>(exception.Message) ?? []),

            ConflictException => (
                StatusCodes.Status409Conflict, JsonSerializer.Deserialize<Error[]>(exception.Message) ?? []),

            FailureException => (
                StatusCodes.Status500InternalServerError, JsonSerializer.Deserialize<Error[]>(exception.Message) ?? []),

            NotFoundException => (
                StatusCodes.Status404NotFound, JsonSerializer.Deserialize<Error[]>(exception.Message) ?? []),

            ValidationException => (
                StatusCodes.Status400BadRequest, JsonSerializer.Deserialize<Error[]>(exception.Message) ?? []),

            _ => (StatusCodes.Status500InternalServerError, [Error.Failure("server.internal", "Something went wrong")])
        };

        Envelope envelope = Envelope.Error(new Errors(errors));
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(envelope);
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this WebApplication app) =>
        app.UseMiddleware<ExceptionMiddleware>();
}