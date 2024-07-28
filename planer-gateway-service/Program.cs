using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder();
configurationBuilder
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddJsonFile("ocelot.auth.json", optional: false, reloadOnChange: true)
    .AddJsonFile("ocelot.mailbox.json", optional: false, reloadOnChange: true)
    .AddJsonFile("ocelot.email-service.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(configurationBuilder.Build());

var corsAllowedOrigins = Environment.GetEnvironmentVariable("CORS_ALLOWED_ORIGINS") ?? throw new Exception("CORS_ALLOWED_ORIGINS is not set");
builder.Services.AddCors(setup =>
    {
        setup.AddDefaultPolicy(options =>
        {
            options.AllowAnyHeader();
            options.WithOrigins(corsAllowedOrigins);
            options.AllowAnyMethod();
        });
    });
var app = builder.Build();

app.UseCors();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

await app.UseOcelot();

app.MapGet("/", () => "Gateway works!");

app.Run();