using Library2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library2.Database;

public class LibraryContext : DbContext
{
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public LibraryContext(DbContextOptions opts) : base(opts)
    {

    }
}
