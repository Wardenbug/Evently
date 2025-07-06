using Evently.Api.Extensions;
using Evently.Modules.Events.Api;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEventsModule(builder.Configuration);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.ApplyMigrations();

    app.MapScalarApiReference();
}

EventsModule.MapEndpoints(app);


await app.RunAsync();
