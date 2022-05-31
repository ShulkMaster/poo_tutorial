using Microsoft.Extensions.Configuration.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Pokedex.Data;

public static class DatabaseBuilder
{
    private static bool wasCreate;
    public static PokedexContext GetInstance()
    {
        var opts = new JsonSerializerOptions();
        opts.PropertyNameCaseInsensitive = true;
        var stream = File.OpenRead("appsettings.json");
        var configs = JsonSerializer.Deserialize<AppSettings>(stream,opts);
        stream.Close();
        stream.Dispose();
        DbContextOptions<PokedexContext> options;
        var optionsbuilder = new DbContextOptionsBuilder<PokedexContext>();
        // options = optionsbuilder.UseSqlServer(configs.Local).Options;
        options = optionsbuilder.UseSqlite(configs.Lite).Options;
        var db = new PokedexContext(options);
        if (!wasCreate) {
            db.Database.EnsureCreated();
            wasCreate = true;
        }
        return db;
    }
}

public class AppSettings
{
    public string Local { get; set; } = string.Empty;
    public string Lite { get; set; } = string.Empty;
}