using MediatR;
using Microsoft.EntityFrameworkCore;
using movieApp.Application.Commands;
using movieApp.Application.DTOs;

namespace movieApp.Application.Handlers;

public class UpdateMovieDetailsCommandHandler : IRequestHandler<UpdateMovieDetailsCommand, MovieDto?>
{
    private readonly IApplicationDbContext _context;

    public UpdateMovieDetailsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MovieDto?> Handle(UpdateMovieDetailsCommand request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (movie == null)
        {
            // Or throw a NotFoundException
            return null;
        }

        movie.Title = request.Title;
        movie.Description = request.Description;
        movie.ReleaseYear = request.ReleaseYear;
        movie.Genre = request.Genre;
        movie.Director = request.Director;
        movie.RuntimeMinutes = request.RuntimeMinutes;

        await _context.SaveChangesAsync(cancellationToken);

        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            ReleaseYear = movie.ReleaseYear,
            Genre = movie.Genre,
            Director = movie.Director,
            RuntimeMinutes = movie.RuntimeMinutes,
            IsWatched = movie.IsWatched,
            
        };
    }
}
