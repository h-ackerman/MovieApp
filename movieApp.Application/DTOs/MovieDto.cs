using movieApp.Domain;

namespace movieApp.Application.DTOs;

public class MovieDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int ReleaseYear { get; set; }
    public Genre Genre { get; set; }
    public string? Director { get; set; }
    public int? RuntimeMinutes { get; set; }
    
    public bool IsWatched { get; set; }
}
