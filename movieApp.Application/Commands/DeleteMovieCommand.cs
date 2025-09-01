using MediatR;

namespace movieApp.Application.Commands;

public class DeleteMovieCommand : IRequest
{
    public Guid Id { get; set; }
}
