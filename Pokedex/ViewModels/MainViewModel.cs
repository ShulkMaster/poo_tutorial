using Pokedex.Api.Request;
using Pokedex.Repositories;
using Pokedex.ViewModels.BindModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Pokedex.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private CancellationTokenSource? _pokemonSource;

    private PokeSource _source = new PokeSource();

    public event PropertyChangedEventHandler? PropertyChanged;

    public PokeSource Source
    {
        get { return _source; }
        private set
        {
            _source = value;
            NotifyPropertyChanged();
        }
    }

    public void CancellPokemonLoad()
    {
        _pokemonSource?.Cancel();
    }

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void LoadPokemon(int newPage, int size)
    {
        if (_pokemonSource is not null) { return; }
        _pokemonSource = new CancellationTokenSource();
        var task = Task.Run(async () =>
        {
            try
            {
                var q = new QueryParams(size);
                q.SetPage(newPage);
                var r = new PokeRepo();
                var sRepo = new SpriteRepository();
                var data = await r.FindRangeAsync(q, _pokemonSource.Token);
                var pics = await sRepo.GetAllDefaultSprites(data.Pokemons, _pokemonSource.Token);
                return new PokeSource(data, pics, q);
            }
            catch (TaskCanceledException) { }
            catch (OperationCanceledException) { }
            finally
            {
                _pokemonSource.Dispose();
                _pokemonSource = null;
            }
            return new PokeSource();
        });
        task.GetAwaiter().OnCompleted(() => { Source = task.Result; });
    }
}
