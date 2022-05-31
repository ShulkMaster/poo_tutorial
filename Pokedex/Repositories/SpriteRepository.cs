namespace Pokedex.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using Resources;
using HttpBitmapTask = Task<(int id, Bitmap? pic)>;
using PokeTupleList = Task<List<(string SpriteName, Bitmap? Pic)>>;

public class SpriteRepository
{
    private const string CacheDir = "cache/img";
    private readonly Bitmap errorIcon = Images.NoticeError;

    private static string MakePath(Pokemon pokemon) => Path.Combine(CacheDir, pokemon.Name);
    private static string MakeFilePath(string basePath, string sprite, string url)
    {
        var ext = Path.GetExtension(url);
        return Path.Combine(basePath, $"{sprite}{ext}");
    }

    private static string MakeFilePath(Pokemon poke, string sprite, string url)
    {
        var ext = Path.GetExtension(url);
        return Path.Combine(MakePath(poke), $"{sprite}{ext}");
    }

    private static Bitmap? ReadCached(Pokemon pokemon, string sprite, string? url)
    {
        if (url is null) return null;
        var path = MakePath(pokemon);
        Directory.CreateDirectory(path);
        var filePath = MakeFilePath(path, sprite, url);
        FileStream? stream = null; 
        try
        {
            stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var image = new Bitmap(stream);
            return image;
        }
        catch (FileNotFoundException) { return null; }
        // if the stream of the file is corrupted or cant be read
        catch (ArgumentException) { return null; }
        finally
        {
            stream?.Close();
            stream?.Dispose();
        }
    }

    private static async HttpBitmapTask ReadHttp(Pokemon pokemon, string sprite, string? url, CancellationToken token)
    {
        if (url is null) return (pokemon.Id, null);
        var client = new HttpClient();
        var stream = await client.GetStreamAsync(url, token);
        var path = MakeFilePath(pokemon, sprite, url);
        var fileStream = File.Create(path);
        await stream.CopyToAsync(fileStream, token);
        stream.Dispose();
        stream.Close();
        var picture = new Bitmap(fileStream);
        fileStream.Flush();
        fileStream.Dispose();
        fileStream.Close();
        return (pokemon.Id, picture);
    }

    public async Task<Dictionary<int, Bitmap>> GetAllDefaultSprites(List<Pokemon?> pokemons, CancellationToken token)
    {
        var tasklist = new List<HttpBitmapTask>();
        var images = new Dictionary<int, Bitmap>();
        const string spriteName = nameof(Sprite.HomeDefault);
        var _client = new HttpClient();
        foreach (var poke in pokemons)
        {
            if (poke is null) { continue; }
            var url = poke.Sprite.HomeDefault;
            var cached = ReadCached(poke, spriteName, url);
            if (cached is not null)
            {
                images.Add(poke.Id, cached);
            }
            else
            {
                tasklist.Add(ReadHttp(poke, spriteName, url, token));
            }
        }
        await Task.WhenAll(tasklist);

        tasklist.ForEach(task => images.Add(task.Result.id, task.Result.pic ?? errorIcon));
        return images;
    }


    public async PokeTupleList GetAllSprites(Pokemon pokemon, CancellationToken token)
    {
        await Task.Delay(3000, token);
        var listUrl = new List<(string sprite, string? url)>();
        var imgs = new List<(string SpriteName, Bitmap? Pic)>(10);
        var sp = pokemon.Sprite;
        listUrl.Add((nameof(Sprite.BackDefault), sp.BackDefault));
        listUrl.Add((nameof(Sprite.BackFemale), sp.BackFemale));
        listUrl.Add((nameof(Sprite.BackShiny), sp.BackShiny));
        listUrl.Add((nameof(Sprite.BackShinyFemale), sp.BackShinyFemale));
        listUrl.Add((nameof(Sprite.FrontDefault), sp.FrontDefault));
        listUrl.Add((nameof(Sprite.FrontFemale), sp.FrontFemale));
        listUrl.Add((nameof(Sprite.FrontShiny), sp.FrontShiny));
        listUrl.Add((nameof(Sprite.FrontShinyFemale), sp.FrontShinyFemale));
        listUrl.Add((nameof(Sprite.HomeDefault), sp.HomeDefault));
        listUrl.Add((nameof(Sprite.HomeFemale), sp.HomeFemale));

        foreach(var tupla in listUrl)
        {
            var bitmap = ReadCached(pokemon, tupla.sprite, tupla.url);
            if (bitmap != null) { 
                imgs.Add((tupla.sprite, bitmap));
                continue;
            }

            var httpImage = await ReadHttp(pokemon, tupla.sprite, tupla.url, token);
            imgs.Add((tupla.sprite, httpImage.pic));
        }
        return imgs;
    }
}
