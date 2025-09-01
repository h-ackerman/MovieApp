using Xunit;
using Moq;
using FluentAssertions;
using movieApp.Application.Commands;
using movieApp.Application.Handlers;
using movieApp.Application; // For IApplicationDbContext
using movieApp.Domain; // For Movie
using MediatR; // For IPublisher
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // For SaveChangesAsync

namespace movieApp.Tests.Application.Commands
{
    public class CreateMovieCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidCommand_ShouldAddMovieToContextAndSaveChanges()
        {
            // Arrange
            var mockContext = new Mock<IApplicationDbContext>();
            var mockPublisher = new Mock<IPublisher>();

            // Setup DbContext to return a DbSet that can be added to
            var mockDbSet = new Mock<DbSet<Movie>>();
            mockContext.Setup(c => c.Movies).Returns(mockDbSet.Object);

            var handler = new CreateMovieCommandHandler(mockContext.Object, mockPublisher.Object);

            var command = new CreateMovieCommand
            {
                Title = "Test Movie",
                Description = "A test description",
                ReleaseYear = 2023,
                Genre = "Action", // Use string for genre as per recent changes
                Director = "Test Director",
                RuntimeMinutes = 120
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be(command.Title);
            mockDbSet.Verify(m => m.Add(It.IsAny<Movie>()), Times.Once);
            mockContext.Verify(c => c.SaveChangesAsync(CancellationToken.None), Times.Once);
            mockPublisher.Verify(p => p.Publish(It.IsAny<Domain.DomainEvents.MovieCreatedEvent>(), CancellationToken.None), Times.Once);
        }
    }
}
