using MediatR;

namespace movieApp.Domain.DomainEvents;

public abstract class DomainEvent : INotification
{
    public bool IsPublished { get; set; }
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
