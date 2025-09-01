using MediatR;
using movieApp.Application.DTOs;
using movieApp.Domain;

namespace movieApp.Application.Queries;

public class GetMoviesQuery : IRequest<List<MovieDto>>
{
    public string? Search { get; set; }
    public Genre? Genre { get; set; }
    public bool? IsWatched { get; set; }
    public bool? TopRated { get; set; }
}
