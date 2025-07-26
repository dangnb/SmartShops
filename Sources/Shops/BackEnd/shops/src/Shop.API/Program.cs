using Shop.API.DependencyInjection.Extensions;
using Shop.API.Middleware;
using Shop.Persistence.DependencyInjection.Options;
using Shop.Persistence.DependencyInjection.Extentions;
using Shop.Application.DependencyInjection.Extensions;
using Serilog;
using Shop.Apptication.Exceptions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shop.Persistence.Dapper.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IUserProvider, UserProvider>();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging
    .ClearProviders()
    .AddSerilog();

builder.Services.AddControllers();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddTransient<JwtMiddleware>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//Add Persistence
builder.Services.ConfigureMySqlRetryOptions(builder.Configuration.GetSection(nameof(MySqlRetryOptions)));
builder.Services.AddSqlConfiguration();
builder.Services.AddRepositoryBaseConfiguration();
builder.Services.AddInfrastructureDapper(builder.Configuration);



//Add Application
builder.Services.AddConfigureMediatR();
builder.Services.AddConfigureAutoMapper();

builder.Services.AddSwagger();

builder.Services.AddControllers()
    .AddApplicationPart(Shop.Persentation.AssemblyReference.assembly);

builder.Services.AddAuthenticationJWT(builder.Configuration);



builder.Services
    .AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<JwtMiddleware>();

//app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
{
    app.ConfigureSwagger();
}


try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}

