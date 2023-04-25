using Cinema.Models;

namespace Cinema.Services;

public interface ITokenService
{
    public bool CheckToken(string token);
}

public class TokenService : ITokenService
{
    private readonly ApplicationDbContext _context;

    public TokenService(ApplicationDbContext context)
    {
        _context = context;
    }

    public bool CheckToken(string token)
    {
        TokenModel? result;
        result = _context.TokenModels.FirstOrDefault(x => x.AccessToken == token);

        if (result == null)
            return true;
        return false;
    }
}