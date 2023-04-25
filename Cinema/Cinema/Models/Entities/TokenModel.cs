using System.ComponentModel.DataAnnotations;

namespace Cinema.Models;

public class TokenModel
{
    [Key] public int Id { get; set; }

    public string AccessToken { get; set; }
}