using Microsoft.EntityFrameworkCore;

namespace AvaloniaUI.Database;

public class UsersDbContext : DbContext
{
    private const string connectionString =
        "Host=localhost;Port=5432;Username=user;Password=klimb2028;Database=messenger";

    public DbSet<Users> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(connectionString);
}