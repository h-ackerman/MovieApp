namespace movieApp.Domain.DomainEvents;

public class MovieRatedEvent : DomainEvent
{
    public Guid MovieId { get; }
    public int? Rating { get; }

    public MovieRatedEvent(Guid movieId, int? rating)
    {
        MovieId = movieId;
        Rating = rating;
    }
}
