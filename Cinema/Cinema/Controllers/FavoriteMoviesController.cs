using System.Security.Claims;
using Cinema.Exceptions;
using Cinema.Models;
using Cinema.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[Route("api/favorites")]
[ApiController]
public class FavoriteMoviesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IFavoriteMoviesService _service;
    private readonly ITokenService _tokenService;

    public FavoriteMoviesController(IFavoriteMoviesService service, ITokenService tokenService,
        ApplicationDbContext context)
    {
        _service = service;
        _tokenService = tokenService;
        _context = context;
    }

    //todo вынести в отдельный сервис работу с логаутом
    //todo разобраться c асинхронными функциями и асинхронным вытаскиванием элементов из бд
    //todo отловить ошибки в пагинации
    // todo отловить ошибку с неверным паролем
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

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<MoviesListModel>> Get()
    {
        Check();
        var userId = CheckUserId();
        return Ok(await _service.GetFavorites(userId));
    }

    [HttpPost]
    [Route("{id}/add")]
    [Authorize]
    public async Task<OkResult> UpdateFavorites(Guid id)
    {
        Check();
        var userId = CheckUserId();
        await _service.UpdateFavorites(id, userId);
        return Ok();
    }

    [HttpDelete]
    [Route("{id}/delete")]
    [Authorize]
    public async Task<OkResult> DeleteFavorites(Guid id)
    {
        Check();
        var userId = CheckUserId();
        await _service.DeleteFavorites(id, userId);
        return Ok();
    }
}