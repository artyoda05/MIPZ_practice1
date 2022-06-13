namespace EuroDiffusion.Entities;

internal class TestCase
{
    const int MostCountriesAmount = 20;

    private readonly Dictionary<(int x, int y), City> _cities;

    public TestCase(List<Country> countries)
    {
        if (countries.Count == 0 && countries.Count > MostCountriesAmount)
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
        Validate();

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

    private void Validate()
    {
        var firstCity = _cities.FirstOrDefault().Value;

        var validated = new List<City> { firstCity };

        var validatedLastIteration = new List<City> { firstCity };

        while (true)
        {
            var validatedThisIteration = new List<City>();

            foreach (var city in validatedLastIteration)
            {
                ValidateCity(validated, validatedThisIteration, (city.X + 1, city.Y));
                ValidateCity(validated, validatedThisIteration, (city.X - 1, city.Y));
                ValidateCity(validated, validatedThisIteration, (city.X, city.Y + 1));
                ValidateCity(validated, validatedThisIteration, (city.X, city.Y - 1));
            }

            if (!validatedThisIteration.Any())
            {
                break;
            }

            validatedLastIteration = validatedThisIteration;
        }


        if (validated.Count != _cities.Count)
        {
            throw new Exception("Not all countries are connected!");
        }
    }

    private void ValidateCity(List<City> validated, List<City> validatedThisIteration, (int x, int y) coordinates)
    {
        if (!_cities.TryGetValue(coordinates, out var city) || validated.Contains(city))
        {
            return;
        }
        
        validated.Add(city);
        validatedThisIteration.Add(city);
    }
}
