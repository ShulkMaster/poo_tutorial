using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Library2.Database;

public class AppSettings
{
    public string Local { get; set; } = string.Empty;
    public string Lite { get; set; } = string.Empty;
}

public class LibraryContextBuilder: DbContextOptionsBuilder<LibraryContext>
{
    private readonly string path;
    private AppSettings? settings;

    public LibraryContextBuilder(string path)
    {
        this.path = path;
    }
    
    public async Task LoadFromFile()
    {
        var opts = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var stream = File.OpenRead(path);
        settings = await JsonSerializer.DeserializeAsync<AppSettings>(stream,opts);
        stream.Close();
        await stream.DisposeAsync();
    }

    public LibraryContextBuilder UseSqlite()
    {
       this.UseSqlServer(settings?.Local ?? "");
       return this;
    }
}