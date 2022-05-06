namespace Pokedex.Api;
public static class Endpoints
{
    private const string BaseUrl = "https://pokeapi.co/api/v2";

    public static class Pokemon
    {
        public const string Index = $"{BaseUrl}/pokemon";

        public static string ById(int id) => $"{Index}/{id}";
    }
}
