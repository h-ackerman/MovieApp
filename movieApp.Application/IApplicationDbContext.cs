using Microsoft.EntityFrameworkCore;
using movieApp.Domain;

namespace movieApp.Application;

public interface IApplicationDbContext
{
    DbSet<Movie> Movies { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
