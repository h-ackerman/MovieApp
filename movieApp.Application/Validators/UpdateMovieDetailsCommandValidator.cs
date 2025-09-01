using FluentValidation;
using movieApp.Application.Commands;

namespace movieApp.Application.Validators;

public class UpdateMovieDetailsCommandValidator : AbstractValidator<UpdateMovieDetailsCommand>
{
    public UpdateMovieDetailsCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.ReleaseYear)
            .InclusiveBetween(1900, DateTime.Now.Year + 1);
    }
}
