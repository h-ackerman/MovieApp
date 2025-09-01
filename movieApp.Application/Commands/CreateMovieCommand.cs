using MediatR;
using movieApp.Application.DTOs;
using movieApp.Domain;

namespace movieApp.Application.Commands;

public class CreateMovieCommand : IRequest<MovieDto>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int ReleaseYear { get; set; }
    public string Genre { get; set; } = string.Empty;
    public string? Director { get; set; }
    public int? RuntimeMinutes { get; set; }
}
