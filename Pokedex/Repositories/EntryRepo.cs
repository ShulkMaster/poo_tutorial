namespace Pokedex.Repositories;

using Pokedex.Repositories.Dto;
using Pokedex.Api.Request;
using Pokedex.Api.Reponses;
using Pokedex.Api;
using Microsoft.EntityFrameworkCore;

public class EntryRepo
{
    PokedexContext context = new PokedexContext();

    private static List<Entry> ToEntry(List<ApiEntry> list)
    {
        var entries = new List<Entry>(list.Count);
        for (int i = 0; i < list.Count; i++)
        {
            ApiEntry entry = list[i];
            entries.Add(new Entry
            {
                Name = entry.Name,
                Url = entry.Url,
            });
            string[] paths = entry.Url.Split('/');
            bool success = int.TryParse(paths[^2], out int id);
            if (success)
            {
                entries[i].Id = id;
            }
        }
        return entries;
    }

    public async Task<EntryList> GetEntriesAsync(QueryParams prams, CancellationToken token = default)
    {
        EntryList p = new EntryList();
        if (prams.Offset + prams.Limit > p.Total)
        {
            return p;
        }

        var pokes = await context.Entries
            .Skip(prams.Offset)
            .Take(prams.Limit)
            .OrderBy(e => e.Id)
            .ToListAsync(token);

        if (pokes.Any())
        {
            p.Entries = pokes;
            return p;
        }
        var api = new PokeApi();
        var result = await api.GetEntriesAsync(prams, token);
        var list = ToEntry(result.Results);
        context.Entries.AddRange(list);
        await context.SaveChangesAsync(token);
        p.Entries = list;
        return p;
    }

}
