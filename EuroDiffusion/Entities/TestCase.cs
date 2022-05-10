namespace EuroDiffusion.Entities;

internal class TestCase
{
    private readonly Dictionary<(int x, int y), City> _cities;

    public TestCase(List<Country> countries)
    {
        if (countries.Count == 0 && countries.Count > 20)
        {
            throw new ArgumentException(nameof(countries));
        }

        Countries = countries;
        _cities = countries
            .SelectMany(country => country.Cities)
            .ToDictionary(city => (city.X, city.Y));
    }

    public List<Country> Countries { get; init; }

    public static TestCase InputFromConsole()
    {
        var input = Console.ReadLine();

        if (!int.TryParse(input, out var size))
        {
            throw new ArgumentException(nameof(size));
        }

        if (size <= 0) 
        { 
           return null; 
        }

        var lines = new List<string>();

        for (int i = 0; i < size; i++)
        {
            lines.Add(Console.ReadLine());
        }

        return Parse(lines.ToArray());
    }

    public static TestCase Parse(IEnumerable<string> parse)
    {
        return new TestCase(parse.Select(p => Country.Parse(p)).ToList());
    }

    public TestCaseResult Execute()
    {
        var results = new Dictionary<string, int>();

        for (var days = 0; true; days++)
        {
            foreach (var r in CompletedCountries())
            {
                results.TryAdd(r, days);
            }

            if (results.Count == Countries.Count)
            {
                break;
            }

            foreach (var ((x, y), city) in _cities)
            {
                SendMoney(x, y + 1, city);
                SendMoney(x - 1, y, city);
                SendMoney(x, y - 1, city);
                SendMoney(x + 1, y, city);
            }

            foreach (var (_, city) in _cities)
            {
                city.ApplyIncoming();
            }
        }

        return new TestCaseResult
        {
            Results = results
        };
    }

    private IEnumerable<string> CompletedCountries() =>
        _cities
            .GroupBy(x => x.Value.Country)
            .Select(g => (g.Key, g.All(x => Countries.All(y => x.Value.Coins.TryGetValue(y.Name, out var amount) && amount > 0))))
            .Where(x => x.Item2)
            .Select(x => x.Key);

    private void SendMoney(int x, int y, City city)
    {
        if (_cities.TryGetValue((x, y), out var incoming))
        {
            city.AddIncoming(incoming.GetOutcoming());
        }
    }
}
