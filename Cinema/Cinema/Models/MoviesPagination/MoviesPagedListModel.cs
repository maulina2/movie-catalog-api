namespace Cinema.Models;

public class MoviesPagedListModel
{
    public MoviesPagedListModel(List<Movie> movie, PageInfoModel pageInfoModel)
    {
        var movieList = new List<MovieElementModel>();
        for (var i = 0; i < movie.Count; i++)
        {
            var movieElement = movie[i];
            var movieElementDto = new MovieElementModel(movieElement);
            movieList.Add(movieElementDto);
        }

        Movies = movieList;
        PageInfo = pageInfoModel;
    }

    public List<MovieElementModel>? Movies { get; set; }
    public PageInfoModel PageInfo { get; set; }
}