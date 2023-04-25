using Cinema.Exceptions;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Services;

public interface IReviewService
{
    public Task<ReviewModifyModel> CreateReview(Guid movieId, string userId, ReviewModifyModel review);

    public Task<ReviewModifyModel> EditReview(Guid movieId, string userId, Guid reviewId,
        ReviewModifyModel reviewModel);

    public Task DeleteReview(Guid movieId, string userId, Guid reviewId);
}

public class ReviewService : IReviewService
{
    private readonly ApplicationDbContext _context;

    public ReviewService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReviewModifyModel> CreateReview(Guid movieId, string userId, ReviewModifyModel review)
    {
        var user = _context.Users.FirstOrDefault(x => x.Username == userId);

        if (IsReviewExists(movieId, user)) throw new ObjectExistsException("Review is exists");

        await _context.Reviews.AddAsync(new Review
        {
            Id = Guid.NewGuid(),
            IsAnonymous = review.IsAnonymous,
            ReviewText = review.ReviewText,
            Rating = review.Rating,
            MovieId = movieId,
            CreateDateTime = DateTime.UtcNow,
            Author = user
        });
        await _context.SaveChangesAsync();

        return review;
    }

    public async Task<ReviewModifyModel> EditReview(Guid movieId, string userId, Guid reviewId,
        ReviewModifyModel reviewModel)
    {
        var review = FindReview(reviewId);
        CheckRights(userId, review.Author);

        review.IsAnonymous = reviewModel.IsAnonymous;
        review.Rating = reviewModel.Rating;
        review.ReviewText = reviewModel.ReviewText;

        await _context.SaveChangesAsync();

        return reviewModel;
    }

    public async Task DeleteReview(Guid movieId, string userId, Guid reviewId)
    {
        var review = FindReview(reviewId);
        CheckRights(userId, review.Author);

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
    }

    private bool IsReviewExists(Guid movieId, User user)
    {
        return _context.Reviews.Any(x => x.MovieId == movieId && x.Author == user);
    }

    public void CheckRights(string userId, User author)
    {
        var user = _context.Users.FirstOrDefault(x => x.Username == userId);
        if (author != user) throw new NotPermissionException();
    }

    public Review FindReview(Guid reviewId)
    {
        var review = _context.Reviews.Include(x => x.Author).FirstOrDefault(x => x.Id == reviewId);
        if (review == null) throw new ReviewNotFoundException();
        return review;
    }
}