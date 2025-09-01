namespace movieApp.Domain.DomainEvents;

public class MovieCreatedEvent : DomainEvent
{
    public Guid MovieId { get; }

    public MovieCreatedEvent(Guid movieId)
    {
        MovieId = movieId;
    }
}
