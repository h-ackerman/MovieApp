namespace movieApp.Domain.DomainEvents;

public class MovieWatchedEvent : DomainEvent
{
    public Guid MovieId { get; }
    public bool IsWatched { get; }

    public MovieWatchedEvent(Guid movieId, bool isWatched)
    {
        MovieId = movieId;
        IsWatched = isWatched;
    }
}
