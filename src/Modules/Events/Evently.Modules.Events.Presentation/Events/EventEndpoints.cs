using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;
public static class EventEndpoints
{
    public static void MapEndpoins(IEndpointRouteBuilder app)
    {
        CreateEvent.MapEndpoints(app);
        GetEvent.MapEndpoint(app);
    }
}
