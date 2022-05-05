namespace EuroDiffusion.Entities;

internal class TestCase
{
    private readonly Dictionary<(int x, int y), City> _cities =
        new Dictionary<(int x, int y), City>();

    public List<Country> Countries { get; set; } = new List<Country>();

    public static TestCase InputFromConsole()
    {
        var input = Console.ReadLine();

        if (!int.TryParse(input, out var size))
        {
            throw new Exception("testcase");
        }

        if (size <= 0)
            return null;

        var testCase = new List<string> { input };

        for (int i = 0; i < size; i++)
        {
            testCase.Add(Console.ReadLine());
        }

        return Parse(testCase.ToArray());
    }

    public static TestCase Parse(string[] parse)
    {
        var testCase = new TestCase();

        if (!int.TryParse(parse[0], out var size))
        {
            throw new Exception("testcase");
        }

        if (size <= 0)
            return null;

        foreach (var c in parse.Skip(1))
        {
            testCase.Countries.Add(Country.Parse(c));
        }

        return testCase;
    }

    public TestCaseResult Execute()
    {
        if (Countries.Count == 1)
        {
            return new TestCaseResult
            {
                Result = new Dictionary<string, int>
                {
                    { Countries[0].Name, 0 }
                }
            };
        }

        PopulateCities();

        var result = new Dictionary<string, int>();

        var days = 0;

        while (true)
        {
            foreach (var r in CompletedCountries)
            {
                result.TryAdd(r, days);
            }

            if (result.Count == Countries.Count)
            {
                break;
            }

            days++;

            foreach (var city in _cities)
            {
                SendMoney(city.Key.x, city.Key.y + 1, city.Value);
                SendMoney(city.Key.x - 1, city.Key.y, city.Value);
                SendMoney(city.Key.x, city.Key.y - 1, city.Value);
                SendMoney(city.Key.x + 1, city.Key.y, city.Value);
            }

            foreach (var (_, city) in _cities)
            {
                city.ApplyIncoming();
            }
        }

        return new TestCaseResult
        {
            Result = result
        };
    }

    private void PopulateCities()
    {
        var cities = Countries
            .SelectMany(country => country.Cities)
            .OrderBy(city => city.Y)
            .ThenBy(city => city.X);

        foreach (var city in cities)
        {
            _cities.Add((city.X, city.Y), city);
        }
    }

    private IEnumerable<string> CompletedCountries =>
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
