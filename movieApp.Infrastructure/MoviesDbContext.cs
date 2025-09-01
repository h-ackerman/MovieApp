using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using movieApp.Application;
using movieApp.Domain;
using movieApp.Domain.DomainEvents;

namespace movieApp.Infrastructure;

public class MoviesDbContext : DbContext, IApplicationDbContext
{
    private readonly IPublisher _publisher;

    public MoviesDbContext(DbContextOptions<MoviesDbContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    public DbSet<Movie> Movies => Set<Movie>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var events = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var @event in events)
        {
            @event.IsPublished = true;
            await _publisher.Publish(@event, cancellationToken);
        }

        return result;
    }
}
