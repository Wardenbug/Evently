using Automatonymous;
using Evently.Modules.Events.Domain.Events;
using MassTransit;

namespace Evently.Modules.Events.Presentation.Events.CancelEventSaga;
public class CancelEventState : SagaStateMachineInstance, ISagaVersion
{
    public Guid CorrelationId { get; set; }
    public int Version { get; set; }

    public string CurrentState { get; set; }

    public int CancellationCompletedStatus { get; set; }
}
