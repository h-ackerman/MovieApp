using MediatR;
using Microsoft.EntityFrameworkCore;
using movieApp.Application.Commands;

namespace movieApp.Application.Handlers;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteMovieCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (movie != null)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
