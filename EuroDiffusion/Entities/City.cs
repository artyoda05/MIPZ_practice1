using EuroDiffusion.Utilities;

namespace EuroDiffusion.Entities;

internal class City
{
    const int CoinsAmount = 1000000;
    const int RepresentativePortionSize = 1000;

    private readonly Dictionary<string, int> _difference = new Dictionary<string, int>();

    public City(string country, int x, int y)
    {
        Country = country;
        X = x;
        Y = y;

        Coins = new Dictionary<string, int>
        {
            { country, CoinsAmount }
        };
    }

    public string Country { get; }

    public int X { get; }

    public int Y { get; }

    public Dictionary<string, int> Coins { get; }

    public void AddIncoming(Dictionary<string, int> incoming)
    {
        foreach (var pair in incoming)
        {
            _difference.AddCoins(pair.Key, pair.Value);
        }
    }

    public Dictionary<string, int> GetOutcoming()
    {
        var dict = new Dictionary<string, int>();

        foreach (var pair in Coins)
        {
            dict.Add(pair.Key, pair.Value / RepresentativePortionSize);

            _difference.AddCoins(pair.Key, -dict[pair.Key]);
        }

        return dict;
    }

    public void ApplyIncoming()
    {
        foreach (var pair in _difference)
        {
            Coins.AddCoins(pair.Key, pair.Value);
        }

        _difference.Clear();
    }
}
