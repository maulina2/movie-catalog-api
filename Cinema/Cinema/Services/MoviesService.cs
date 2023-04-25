using Cinema.Exceptions;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services;

public interface IMoviesService
{
    public Task<MovieDetails> GetMovies(Guid movieId);
    public Task<MoviesPagedListModel> GetPages(int page);
}

public class MoviesService : IMoviesService
{
    private readonly ApplicationDbContext _context;

    public MoviesService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MovieDetails> GetMovies(Guid movieId)
    {
        var movie = _context.Movies.Include(x => x.Genre).Include(x => x.Reviews)
            .ThenInclude(review => review.Author).FirstOrDefault(m => m.Id == movieId);

        if (movie == null) throw new MovieNotFoundException();

        var model = new MovieDetails(movie);

        return model;
    }

    public async Task<MoviesPagedListModel> GetPages(int page)
    {
        var moviesCount = _context.Movies.Count();
        var pageInfoModel = new PageInfoModel(page, moviesCount);
        var size = pageInfoModel.pageSize;
        var start = (page - 1) * size;
        var movies = _context.Movies.Include(x => x.Genre).Include(x => x.Reviews).OrderBy(x => x.Name).Skip(start)
            .Take(size).ToList();

        var pageMovie = new MoviesPagedListModel(movies, pageInfoModel);
        return pageMovie;
    }
}