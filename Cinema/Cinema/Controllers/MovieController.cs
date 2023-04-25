using Cinema.Models;
using Cinema.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[Route("api/movies")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMoviesService _service;

    public MovieController(IMoviesService service, ApplicationDbContext context)
    {
        _service = service;
        _context = context;
    }

    [HttpGet]
    [Route("{page}")]
    public async Task<ActionResult<MoviesPagedListModel>> GetPages(int page)
    {
        return Ok(await _service.GetPages(page));
    }

    [HttpGet]
    [Route("details/{id}")]
    public async Task<ActionResult<MovieDetails>> Get(Guid id)
    {
        return Ok(await _service.GetMovies(id));
    }
}