using MediatR;

namespace movieApp.Application.Commands;

public class MarkMovieWatchedCommand : IRequest
{
    public Guid Id { get; set; }
    public bool IsWatched { get; set; }
}
