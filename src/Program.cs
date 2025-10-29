using BlogApi.Infrastructure;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSerilog((services, ls) => ls
            .ReadFrom.Configuration(builder.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Console());

    SqlConnectionStringBuilder sqlConnectionStringBuilder = new(builder.Configuration.GetConnectionString("BlogContext"))
    {
        Password = builder.Configuration["BlogContext:Password"]
    };

    builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(sqlConnectionStringBuilder.ConnectionString));

    builder.Services.AddControllers();
    builder.Services.AddOpenApi();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
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