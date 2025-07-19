using Evently.Api.Extensions;
using Evently.Modules.Events.Infrastructure;
using Scalar.AspNetCore;
using Evently.Common.Application;
using Evently.Common.Infrastructure;
using Serilog;
using Evently.Api.Middleware;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Evently.Common.Presentation.Endpoints;
using Evently.Modules.Users.Infrastructure;
using Evently.Modules.Ticketing.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;
builder.Services.AddApplication([
    Evently.Modules.Events.Application.AssemblyReference.Assembly,
    Evently.Modules.Users.Application.AssemblyReference.Assembly,
    Evently.Modules.Ticketing.Application.AssemblyReference.Assembly,
]);
builder.Services.AddInfrastructure(
    [TicketingModule.ConfigureConsumers],
    databaseConnectionString,
    redisConnectionString
    );

builder.Configuration.AddModuleConfiguration(["events", "users", "ticketing"]);

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddRedis(redisConnectionString)
    .AddUrlGroup(
    new Uri(builder.Configuration.GetValue<string>("KeyCloak:HealthUrl")!),
    HttpMethod.Get,
    "keycloak");

builder.Services.AddEventsModule(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddTicketingModule(builder.Configuration);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.ApplyMigrations();

    app.MapScalarApiReference();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapEndpoints();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

await app.RunAsync();
