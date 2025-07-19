using MediatR;

namespace Ordering.Domain.Abstractions;

// using of InOtificated  beacure it allwing domain events to be dispatched through the mediator handlers
public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
}
