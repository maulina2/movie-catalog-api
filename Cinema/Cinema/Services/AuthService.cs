using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web.Helpers;
using Cinema.Exceptions;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Cinema.Services;

public interface IAuthService
{
    public Task Register(RegisterDto model);
    public Task<ActionResult<TokenModel>> Login(LoginDto model);
    public Task Logout(string invalidToken);
}

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ActionResult<TokenModel>> Login(LoginDto model)
    {
        var identity = await GetIdentity(model.userName, model.password);
        if (identity == null) throw new UserNotFoundException();
        var user = _context.Users.FirstOrDefault(x => x.Username == model.userName);
        if (user == null) throw new ValidationException("Wrong username");
        if (!Crypto.VerifyHashedPassword(user.Password, model.password))
            throw new ValidationException("Wrong password");
        var now = DateTime.UtcNow;
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            JwtConfigurations.Issuer,
            JwtConfigurations.Audience,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(JwtConfigurations.Lifetime)),
            signingCredentials: new SigningCredentials(JwtConfigurations.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        var response = new
        {
            access_token = encodedJwt
        };
        return new JsonResult(response);
    }

    public async Task Register(RegisterDto model)
    {
        if (IsUserExists(model)) throw new ObjectExistsException("User exists");
        await _context.Users.AddAsync(new User
        {
            Id = Guid.NewGuid(),
            Username = model.Username,
            Name = model.Name,
            Password = Crypto.HashPassword(model.Password),
            BirthDate = model.BirthDate,
            Email = model.Email,
            Gender = model.Gender
        });
        await _context.SaveChangesAsync();
    }

    public async Task Logout(string invalidToken)
    {
        if (invalidToken is null) throw new ValidationException("Bad username");

        await _context.TokenModels.AddAsync(new TokenModel
        {
            AccessToken = invalidToken
        });

        await _context.SaveChangesAsync();
    }

    private async Task<ClaimsIdentity> GetIdentity(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(x => x.Username == username);

        if (user == null) throw new ValidationException("Wrong username");

        if (!Crypto.VerifyHashedPassword(user.Password, password)) throw new ValidationException("Wrong password");
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, user.Username)
        };

        var claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        return claimsIdentity;
    }

    private bool IsUserExists(RegisterDto newUser)
    {
        return _context.Users.Any(user => user.Email == newUser.Email || user.Username == newUser.Username);
    }
}