using Microsoft.EntityFrameworkCore;
using Messenger.Core.Models;


namespace Messenger.Core.DataBase;

public class DataBaseContext : DbContext
{
    public DbSet<UserModel> Users { get; set; }

    public DataBaseContext (DbContextOptions<DataBaseContext> options) : base(options) 
    { 
        Database.EnsureCreated();
    }
}
