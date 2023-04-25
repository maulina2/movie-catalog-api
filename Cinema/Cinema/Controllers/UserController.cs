using Cinema.Exceptions;
using Cinema.Models;
using Cinema.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[Route("api/account/profile")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _service;
    private readonly ITokenService _tokenService;

    public UserController(IUserService service, ITokenService tokenService, ApplicationDbContext context)
    {
        _service = service;
        _tokenService = tokenService;
        _context = context;
    }

    private void Check()
    {
        var token = HttpContext.Request.Headers.Authorization.ToString().Split(' ');
        var result = _tokenService.CheckToken(token[1]);

        if (!result) throw new UnauthorizedException();
    }

    [HttpGet]
    [Authorize]
    public ActionResult<List<ProfileDto>> Get()
    {
        Check();
        return Ok(_service.GetUsers());
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<ProfileDto>> PutUser(ProfileDto profile)
    {
        Check();
        await _service.PutUser(profile);
        return Ok(profile);
    }
}