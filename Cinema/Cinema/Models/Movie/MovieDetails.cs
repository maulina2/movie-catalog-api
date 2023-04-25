namespace Cinema.Models;

public class MovieDetails
{
    public MovieDetails(Movie movie)
    {
        Id = movie.Id;
        Name = movie.Name;
        Poster = movie.Poster;
        Description = movie.Description;
        Year = movie.Year;
        Country = movie.Country;
        Time = movie.Time;
        Tagline = movie.Tagline;
        Director = movie.Director;
        Budget = movie.Budget;
        Fees = movie.Fees;
        AgeLimit = movie.AgeLimit;
        var genres = movie.Genre.Select(genre => new GenreDto(genre)).ToList();
        Genre = genres;
        var reviews = movie.Reviews.Select(review => new ReviewDto(review)).ToList();
        Reviews = reviews;
    }

    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Poster { get; set; }
    public string? Description { get; set; }
    public int Year { get; set; }
    public string? Country { get; set; }
    public int Time { get; set; }
    public string? Tagline { get; set; }
    public string? Director { get; set; }
    public int? Budget { get; set; }
    public int? Fees { get; set; }
    public int AgeLimit { get; set; }
    public List<GenreDto> Genre { get; set; }
    public List<ReviewDto> Reviews { get; set; }
}