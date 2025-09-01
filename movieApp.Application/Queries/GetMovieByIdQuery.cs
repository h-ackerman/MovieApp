using MediatR;
using movieApp.Application.DTOs;

namespace movieApp.Application.Queries;

public class GetMovieByIdQuery : IRequest<MovieDto?>
{
    public Guid Id { get; set; }
}
