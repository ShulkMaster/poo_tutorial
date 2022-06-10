using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Library2.Database;

public class DesignFactory : IDesignTimeDbContextFactory<LibraryContext>
{

    public LibraryContext CreateDbContext(string[] args)
    {
        foreach (var arg in args)
        {
            Console.WriteLine(arg);
        }
        DbContextOptions<LibraryContext> options;
        var optionsbuilder = new DbContextOptionsBuilder<LibraryContext>();
        options = optionsbuilder.UseSqlServer("Data Source=localhost;initial catalog=library;Trusted_Connection=True;MultipleActiveResultSets=True;").Options;
        var db = new LibraryContext(options);
        return db;
    }
}
