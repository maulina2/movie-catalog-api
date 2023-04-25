using Microsoft.EntityFrameworkCore;

namespace Cinema.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<TokenModel> TokenModels { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
    }
}