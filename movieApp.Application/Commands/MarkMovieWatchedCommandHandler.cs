using MediatR;
using Microsoft.EntityFrameworkCore;
using movieApp.Application.Commands;
using movieApp.Domain.DomainEvents;

namespace movieApp.Application.Handlers;

public class MarkMovieWatchedCommandHandler : IRequestHandler<MarkMovieWatchedCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public MarkMovieWatchedCommandHandler(IApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task Handle(MarkMovieWatchedCommand request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (movie != null)
        {
            movie.IsWatched = request.IsWatched;
            await _context.SaveChangesAsync(cancellationToken);
            await _publisher.Publish(new MovieWatchedEvent(movie.Id, movie.IsWatched), cancellationToken);
        }
    }
}
