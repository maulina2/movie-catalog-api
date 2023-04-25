using System.ComponentModel.DataAnnotations;

namespace Cinema.Models;

public class ReviewModifyModel
{
    public ReviewModifyModel(Review review)
    {
        ReviewText = review.ReviewText;
        Rating = review.Rating;
        IsAnonymous = review.IsAnonymous;
    }

    public ReviewModifyModel()
    {
        ReviewText = "";
    }

    public string ReviewText { get; set; }

    [Required] [Range(0, 10)] public int Rating { get; set; }

    public bool IsAnonymous { get; set; }
}