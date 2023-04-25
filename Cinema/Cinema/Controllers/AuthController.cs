using Cinema.Models;
using Cinema.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers;

[Route("api/account")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public IAuthService Service;

    public AuthController(IAuthService service, ApplicationDbContext context)
    {
        Service = service;
        _context = context;
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenModel>> LoginUser([FromBody] LoginDto model)
    {
        return await Service.Login(model);
    }

    [Route("register")]
    [HttpPost]
    public async Task<ActionResult<TokenModel>> Register([FromBody] RegisterDto model)
    {
        await Service.Register(model);
        var token = await LoginUser(new LoginDto
        {
            userName = model.Username,
            password = model.Password
        });
        return token;
    }

    [Route("logout")]
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var token = HttpContext.Request.Headers.Authorization.ToString().Split(' ');
        await Service.Logout(token[1]);
        return Ok();
    }
}