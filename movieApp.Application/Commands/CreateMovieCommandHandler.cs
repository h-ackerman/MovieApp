using MediatR;
using movieApp.Application.Commands;
using movieApp.Application.DTOs;
using movieApp.Domain;
using movieApp.Domain.DomainEvents;

namespace movieApp.Application.Handlers;

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, MovieDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IPublisher _publisher;

    public CreateMovieCommandHandler(IApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task<MovieDto> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<Genre>(request.Genre, true, out var parsedGenre))
        {
            // Default to a genre if parsing fails. Alternatively, you could throw a validation exception.
            parsedGenre = Genre.Action;
        }

        var movie = new Movie
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            ReleaseYear = request.ReleaseYear,
            Genre = parsedGenre,
            Director = request.Director,
            RuntimeMinutes = request.RuntimeMinutes,
            
        };

        _context.Movies.Add(movie);

        await _context.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new MovieCreatedEvent(movie.Id), cancellationToken);

        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            ReleaseYear = movie.ReleaseYear,
            Genre = movie.Genre,
            Director = movie.Director,
            RuntimeMinutes = movie.RuntimeMinutes
        };
    }
}
