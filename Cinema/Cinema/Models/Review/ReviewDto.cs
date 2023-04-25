using System.ComponentModel.DataAnnotations;

namespace Cinema.Models;

public class ReviewDto
{
    public ReviewDto(Review review)
    {
        Id = review.Id;
        ReviewText = review.ReviewText;
        Rating = review.Rating;
        CreateDateTime = review.CreateDateTime;
        IsAnonymous = review.IsAnonymous;

        var user = new UserShortModel(review.Author);
        Author = user;
    }

    public Guid Id { get; set; }
    public string? ReviewText { get; set; }

    [Required] [Range(0, 10)] public int Rating { get; set; }

    public DateTime CreateDateTime { get; set; }
    public bool IsAnonymous { get; set; }
    public UserShortModel Author { get; set; }
}