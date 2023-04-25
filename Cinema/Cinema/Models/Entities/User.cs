namespace Cinema.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Gender Gender { get; set; }

    public string? AvatarLink { get; set; }
    public string? NickName { get; set; }

    public List<Review> Reviews { get; set; }
    public List<Movie> FavoriteMovies { get; set; }
}