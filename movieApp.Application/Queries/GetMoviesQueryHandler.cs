using MediatR;
using Microsoft.EntityFrameworkCore;
using movieApp.Application.DTOs;
using movieApp.Application.Queries;

namespace movieApp.Application.Handlers;

public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, List<MovieDto>>
{
    private readonly IApplicationDbContext _context;

    public GetMoviesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MovieDto>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Movies.AsNoTracking();

        if (!string.IsNullOrEmpty(request.Search))
        {
            query = query.Where(m => m.Title.Contains(request.Search));
        }

        if (request.Genre.HasValue)
        {
            query = query.Where(m => m.Genre == request.Genre.Value);
        }

        if (request.IsWatched.HasValue)
        {
            query = query.Where(m => m.IsWatched == request.IsWatched.Value);
        }

        

        return await query.Select(m => new MovieDto
        {
            Id = m.Id,
            Title = m.Title,
            Description = m.Description,
            ReleaseYear = m.ReleaseYear,
            Genre = m.Genre,
            Director = m.Director,
            RuntimeMinutes = m.RuntimeMinutes,
            IsWatched = m.IsWatched,
            
        }).ToListAsync(cancellationToken);
    }
}
