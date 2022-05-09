using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Api.Request;
public class QueryParams
{
    public const int DefaultLimit = 50;
    private int _offset;
    private int _limit = DefaultLimit;

    public int Offset
    {
        get => _offset;
        set => _offset = Math.Abs(value);
    }

    public int Limit
    {
        get => _limit;
        set => _limit = Math.Abs(value);
    }

    public QueryParams() { }

    public QueryParams(int limit)
    {
        Limit = limit;
    }

    public void SetPage(int page)
    {
        _offset = Math.Max(0, (Math.Abs(page) - 1) * _limit);
    }

    public Dictionary<string, string> AsDictionary()
    {
        return new Dictionary<string, string>
        {
            { nameof(Limit).ToLower(), _limit.ToString() },
            { nameof(Offset).ToLower(), _offset.ToString() },
        };
    }
}
