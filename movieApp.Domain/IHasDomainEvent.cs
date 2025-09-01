using movieApp.Domain.DomainEvents;

namespace movieApp.Domain;

public interface IHasDomainEvent
{
    public List<DomainEvent> DomainEvents { get; set; }
}
