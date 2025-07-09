using Evently.Api.Extensions;
using Evently.Modules.Events.Infrastructure;
using Scalar.AspNetCore;
using Evently.Common.Application;
using Evently.Common.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEventsModule(builder.Configuration);

builder.Services.AddApplication([Evently.Modules.Events.Application.AssemblyReference.Assembly]);
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("Database")!);

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
