namespace Pokedex.Api.Reponses;

using System.Text.Json.Serialization;

public class ApiSprite
{
    public string? BackDefault { get; set; }

    public string? BackFemale { get; set; }

    public string? BackShiny { get; set; }

    public string? BackShinyFemale { get; set; }

    public string? FrontDefault { get; set; }

    public string? FrontFemale { get; set; }

    public string? FrontShiny { get; set; }

    public string? FrontShinyFemale { get; set; }

    public OtherSprite Other { get; set; } = new OtherSprite();

    public class OtherSprite
    {
        public HomeSprite Home { get; set; } = new HomeSprite();

        public WorldSprite DreamWorld { get; set; } = new WorldSprite();
    }

    public class HomeSprite
    {
        public string? FrontDefault { get; set; }

        public string? FrontFemale { get; set; }

        public string? FrontShiny { get; set; }

        public string? FrontShinyFemale { get; set; }
    }

    public class WorldSprite
    {
        public string? FrontDefault { get; set; }
    }
}
