namespace Cinema.Models;

public class ReviewShortModel
{
    public ReviewShortModel(Review review)
    {
        Id = review.Id;
        Rating = review.Rating;
    }

    public Guid Id { get; set; }
    public int Rating { get; set; }
}