namespace Cinema.Models;

public class MovieElementModel
{
    public MovieElementModel(Movie movie)
    {
        Id = movie.Id;
        Name = movie.Name;
        Poster = movie.Poster;
        Year = movie.Year;
        Country = movie.Country;
        var genres = movie.Genre.Select(genre => new GenreDto(genre)).ToList();
        Genre = genres;
        var reviews = movie.Reviews.Select(review => new ReviewShortModel(review)).ToList();
        Reviews = reviews;
    }

    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Poster { get; set; }
    public int Year { get; set; }
    public string? Country { get; set; }
    public List<GenreDto>? Genre { get; set; }
    public List<ReviewShortModel>? Reviews { get; set; }
}