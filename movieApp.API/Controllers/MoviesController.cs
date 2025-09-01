using MediatR;
using Microsoft.AspNetCore.Mvc;
using movieApp.Application.Commands;
using movieApp.Application.Queries;
using movieApp.Application.DTOs;

namespace movieApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MoviesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<MovieDto>>> GetMovies([FromQuery] GetMoviesQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovie(Guid id)
    {
        var movie = await _mediator.Send(new GetMovieByIdQuery { Id = id });
        return movie == null ? NotFound() : Ok(movie);
    }

    [HttpPost]
    public async Task<ActionResult<MovieDto>> CreateMovie(CreateMovieCommand command)
    {
        var movie = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(Guid id, UpdateMovieDetailsCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        var movie = await _mediator.Send(command);

        return movie == null ? NotFound() : NoContent();
    }

    [HttpPatch("{id}/watch")]
    public async Task<IActionResult> MarkMovieWatched(Guid id, MarkMovieWatchedCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(command);
        return NoContent();
    }

    

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(Guid id)
    {
        await _mediator.Send(new DeleteMovieCommand { Id = id });
        return NoContent();
    }
}
