using BlogApi.Application.Services;
using BlogApi.Application.Services.Interfaces;
using BlogApi.Domain.Entities;
using BlogApi.Domain.Interfaces;
using BlogApi.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSerilog(
        (services, ls) =>
            ls
                .ReadFrom.Configuration(builder.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console()
    );

    SqlConnectionStringBuilder sqlConnectionStringBuilder = new(
        builder.Configuration.GetConnectionString("BlogContext")
    )
    {
        Password = builder.Configuration["BlogContext:Password"],
    };

    builder.Services.AddDbContext<BlogContext>(options =>
        options.UseSqlServer(sqlConnectionStringBuilder.ConnectionString)
    );

    builder.Services.AddScoped<IBlogRepository, BlogRepository>();
    builder.Services.AddScoped<IBlogService, BlogService>();

    builder.Services.AddIdentityApiEndpoints<UserModel>().AddEntityFrameworkStores<BlogContext>();

    builder.Services.AddControllers();
    builder.Services.AddOpenApi();
    builder.Services.AddSwaggerGen(c =>
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogApi", Version = "v1" })
    );

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogApi v1"));
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.MapIdentityApi<UserModel>();

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
