using FluentValidation;
using movieApp.Application.Commands;

namespace movieApp.Application.Validators;

public class MarkMovieWatchedCommandValidator : AbstractValidator<MarkMovieWatchedCommand>
{
    public MarkMovieWatchedCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
