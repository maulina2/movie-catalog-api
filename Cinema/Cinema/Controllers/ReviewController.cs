using System.Security.Claims;
using Cinema.Exceptions;
using Cinema.Models;
using Cinema.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[Route("api/movie")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ITokenService _tokenService;
    public IReviewService Service;

    public ReviewController(IReviewService service, ITokenService tokenService, ApplicationDbContext context)
    {
        Service = service;
        _tokenService = tokenService;
        _context = context;
    }

    private string? GetUserIdFromToken()
    {
        var id = User.Claims.FirstOrDefault(claim =>
                claim.Type == ClaimTypes.Name)
            ?.Value;
        return id;
    }

    private string CheckUserId()
    {
        var userId = GetUserIdFromToken();
        if (userId == null) throw new UserNotFoundException();

        return userId;
    }

    private void Check()
    {
        var token = HttpContext.Request.Headers.Authorization.ToString().Split(' ');
        var result = _tokenService.CheckToken(token[1]);

        if (!result) throw new UnauthorizedException();
    }

    [HttpPost]
    [Route("{movieId}/review/add")]
    [Authorize]
    public async Task<ReviewModifyModel> Add(Guid movieId, [FromBody] ReviewModifyModel review)
    {
        Check();
        var userId = CheckUserId();
        return await Service.CreateReview(movieId, userId, review);
    }

    [HttpPut]
    [Route("{movieId}/review/{id}/edit")]
    [Authorize]
    public async Task<ReviewModifyModel> Edit(Guid movieId, Guid id, [FromBody] ReviewModifyModel review)
    {
        Check();
        var userId = CheckUserId();
        return await Service.EditReview(movieId, userId, id, review);
    }

    [HttpDelete]
    [Route("{movieId}/review/{id}/delete")]
    [Authorize]
    public async Task<OkResult> Delete(Guid movieId, Guid id)
    {
        Check();
        var userId = CheckUserId();
        await Service.DeleteReview(movieId, userId, id);
        return Ok();
    }
}