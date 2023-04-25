namespace Cinema.Models;

public class MoviesListModel
{
    public MoviesListModel(List<Movie> movies)
    {
        var movieList = new List<MovieElementModel>();
        foreach (var movieElement in movies)
        {
            var movieElementDto = new MovieElementModel(movieElement);
            movieList.Add(movieElementDto);
        }

        Movies = movieList;
    }

    public List<MovieElementModel> Movies { get; set; }
}