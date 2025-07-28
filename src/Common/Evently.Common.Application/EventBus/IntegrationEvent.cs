namespace Evently.Common.Application.EventBus;

public abstract class IntegrationEvent : IIntegrationEvent
{
    protected IntegrationEvent(Guid id, DateTime occuredOnUtc)
    {
        Id = id;
        OccurredOnUtc = occuredOnUtc;
    }

    public Guid Id { get; init; }
    public DateTime OccurredOnUtc { get; init; }
}
