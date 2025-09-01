using MediatR;
using Microsoft.EntityFrameworkCore;
using movieApp.Application.DTOs;
using movieApp.Application.Queries;

namespace movieApp.Application.Handlers;

public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, MovieDto?>
{
    private readonly IApplicationDbContext _context;

    public GetMovieByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MovieDto?> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies
            .AsNoTracking()
            .Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                ReleaseYear = m.ReleaseYear,
                Genre = m.Genre,
                Director = m.Director,
                RuntimeMinutes = m.RuntimeMinutes,
                IsWatched = m.IsWatched,
                
            })
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return movie;
    }
}
