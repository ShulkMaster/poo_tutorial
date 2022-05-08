using System.Text.Json;
using System.Text.Transformation;

namespace Pokedex.Api;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return name.ToSnakeCase();
    }
}
