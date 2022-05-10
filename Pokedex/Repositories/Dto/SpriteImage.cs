namespace Pokedex.Repositories.Dto;

using Pokedex.Resources;

public class SpriteImage
{
    private static readonly Bitmap bitmap = Images.NoticeError;

    public int Id { get; set; }

    public Bitmap BackDefault { get; set; } = bitmap;

    public Bitmap BackFemale { get; set; } = bitmap;

    public Bitmap BackShiny { get; set; } = bitmap;

    public Bitmap BackShinyFemale { get; set; } = bitmap;

    public Bitmap FrontDefault { get; set; } = bitmap;

    public Bitmap FrontFemale { get; set; } = bitmap;

    public Bitmap FrontShiny { get; set; } = bitmap;

    public Bitmap FrontShinyFemale { get; set; } = bitmap;

    public Bitmap HomeDefault { get; set; } = bitmap;

    public Bitmap HomeFemale { get; set; } = bitmap;

    public Bitmap HomeShiny { get; set; } = bitmap;

    public Bitmap HomeShinyFemale { get; set; } = bitmap;
}
