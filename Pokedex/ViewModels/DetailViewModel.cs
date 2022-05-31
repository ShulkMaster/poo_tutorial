using Pokedex.Repositories;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Pokedex.Resources;
using TupleList = System.Collections.Generic.List<(string SpriteName, System.Drawing.Bitmap? Pic)>;


namespace Pokedex.ViewModels;

public class DetailViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly SpriteRepository imgRepo;
    private TupleList pics = new TupleList();

    public TupleList Pics
    {
        get => pics; set
        {
            pics = value;
            NotifyPropertyChanged();
        }
    }

    public DetailViewModel()
    {
        imgRepo = new SpriteRepository();
    }

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    public void LoadImages(Pokemon p, CancellationToken t)
    {
        var task = imgRepo.GetAllSprites(p, t);
        task.GetAwaiter().OnCompleted(() => {
            try
            {
                Pics = task.Result;

            }
            catch (AggregateException) {
                var x = new TupleList();
                x.Add(("error", Images.NoticeError));
                x.Add(("error", Images.NoticeError));
                x.Add(("error", Images.NoticeError));
                Pics = x;
            }
        });
    }

}
