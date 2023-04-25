using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Cinema.Models;

public class JwtConfigurations
{
    public const string Issuer = "JwtTestIssuer"; // издатель токена
    public const string Audience = "JwtTestClient"; // потребитель токена
    private const string Key = "SuperSecretKeyBazingaLolKek!*228322"; // ключ для шифрации
    public static readonly double Lifetime = 15; // время жизни токена - 5 минут

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}