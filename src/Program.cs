using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSerilog((service, ls) => ls
            .ReadFrom.Configuration(builder.Configuration)
            .ReadFrom.Services(service)
            .Enrich.FromLogContext()
            .WriteTo.Console());

    builder.Services.AddControllers();
    builder.Services.AddOpenApi();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Error(ex, "Terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}