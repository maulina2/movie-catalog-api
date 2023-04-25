namespace Cinema.Models;

public class GenreDto
{
    public GenreDto(Genre genre)
    {
        Id = genre.Id;
        Name = genre.Name;
    }

    public Guid Id { get; set; }
    public string? Name { get; set; }
}