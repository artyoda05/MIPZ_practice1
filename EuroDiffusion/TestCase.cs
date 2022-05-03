using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusion
{
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

            var testCase = new TestCase();

            for (int i = 0; i < size; i++)
            {
                testCase.Countries.Add(Country.InputFromConsole());
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

            FormGraph();

            var results = new Dictionary<string, int>();

            for (var i = 0; true; i++)
            {
                foreach (var city in _cities)
                {
                    SendMoney(city.Key.x, city.Key.y + 1, city.Value);
                    SendMoney(city.Key.x - 1, city.Key.y, city.Value);
                    SendMoney(city.Key.x, city.Key.y - 1, city.Value);
                    SendMoney(city.Key.x + 1, city.Key.y, city.Value);
                   
                }

                foreach (var city in _cities)
                {
                    city.Value.ApplyIncoming();
                }

                var res = _cities
                    .GroupBy(x => x.Value.Country)
                    .Select(g => (g.Key, g.All(x => Countries.All(y => x.Value.Coins.TryGetValue(y.Name, out var amount) && amount > 0))))
                    .Where(x => x.Item2);

                foreach (var r in res)
                {
                    results.TryAdd(r.Key, i + 1);
                }

                if (results.Count == Countries.Count)
                {
                    break;
                }
            }

            return new TestCaseResult
            {
                Result = results
            };
        }

        private void FormGraph()
        {
            var cities = Countries.SelectMany(country => country.Cities)
                .OrderBy(city => city.Y)
                .ThenBy(city => city.X);

            foreach (var city in cities)
            {
                _cities.Add((city.X, city.Y), city);
            }
        }

        private void SendMoney(int x, int y, City city)
        {
            if (_cities.TryGetValue((x, y), out var outcoming))
            {
                //city.AddIncoming(outcoming.GetOutcoming());

                outcoming.AddIncoming(city.GetOutcoming());
            }
        }
    }
}
