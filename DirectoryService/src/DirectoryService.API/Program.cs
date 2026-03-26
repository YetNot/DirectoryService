using System.Globalization;
using DirectoryService.Presentation;
using DirectoryService.Presentation.Middlewares;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting web Application <<DirectoryService>>");

    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Services.AddConfiguration(builder.Configuration);

    WebApplication app = builder.Build();

    app.UseExceptionMiddleware();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "DirectoryService API"));
    }

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application <<DirectoryService>> terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}