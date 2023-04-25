using Cinema.Exceptions;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services;

public interface IFavoriteMoviesService
{
    public Task<MoviesListModel> GetFavorites(string userId);
    public Task UpdateFavorites(Guid moviewId, string userId);
    public Task DeleteFavorites(Guid moviewId, string userId);
}

public class FavoriteMoviesService : IFavoriteMoviesService
{
    private readonly ApplicationDbContext _context;

    public FavoriteMoviesService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MoviesListModel> GetFavorites(string userId)
    {
        var user = FindUser(userId);
        var favorites = FindFavorites(user);
        var favoritesDto = new MoviesListModel(favorites);
        return favoritesDto;
    }

    public async Task UpdateFavorites(Guid moviewId, string userId)
    {
        var user = FindUser(userId);
        var favorites = FindFavorites(user);
        var movie = FindMovie(moviewId);
        if (IsMovieExists(favorites, movie)) throw new ObjectExistsException("this movie is already in your favorites");
        favorites.Add(movie);
        user.FavoriteMovies = favorites;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFavorites(Guid moviewId, string userId)
    {
        var user = FindUser(userId);
        var favorites = FindFavorites(user);
        var movie = FindMovie(moviewId);
        if (!IsMovieExists(favorites, movie)) throw new MovieNotFoundException();
        favorites.Remove(movie);
        user.FavoriteMovies = favorites;
        await _context.SaveChangesAsync();
    }

    private User FindUser(string userId)
    {
        var user = _context.Users.Include(x => x.FavoriteMovies)
            .ThenInclude(m => m.Genre)
            .Include(x => x.FavoriteMovies)
            .ThenInclude(g => g.Reviews).FirstOrDefault(x => x.Username == userId);
        if (user == null) throw new UserNotFoundException();

        return user;
    }

    private List<Movie> FindFavorites(User user)
    {
        var favorites = user.FavoriteMovies;
        return favorites;
    }

    private Movie FindMovie(Guid movieId)
    {
        var movie = _context.Movies.Include(x => x.Genre).Include(x => x.Reviews).FirstOrDefault(m => m.Id == movieId);
        if (movie == null) throw new MovieNotFoundException();
        return movie;
    }

    private bool IsMovieExists(List<Movie> favorites, Movie movie)
    {
        return favorites.Contains(movie);
    }
}