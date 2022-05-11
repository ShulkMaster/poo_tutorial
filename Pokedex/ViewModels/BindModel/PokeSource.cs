using Pokedex.Api.Request;
using Pokedex.Repositories.Dto;
using System.Collections;

namespace Pokedex.ViewModels.BindModel;

public class PokeSource: IReadOnlyList<Pokemon?>
{
    private readonly PokeList l;
    private readonly Dictionary<int, Bitmap> pics;

    public int Total { get; private set; }

    public int TotalPages { get; private set; }

    public int Page { get; private set; }

    public int Count => l.Pokemons.Count;

    public Pokemon? this[int index] => l.Pokemons[index];

    public PokeSource()
    {
        l = new PokeList();
        pics = new Dictionary<int, Bitmap>();
    }

    public PokeSource(PokeList l, Dictionary<int, Bitmap> pics, QueryParams q)
    {
        this.l = l;
        this.pics = pics;
        Total = l.Total;
        TotalPages = (int)Math.Ceiling(l.Total / (float)q.Limit);
        Page = q.Offset / q.Limit + 1;
    }

    public Bitmap GetImage(Pokemon p) => pics[p.Id];

    public IEnumerator<Pokemon?> GetEnumerator()
    {
        return l.Pokemons.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return l.Pokemons.GetEnumerator();
    }
}
