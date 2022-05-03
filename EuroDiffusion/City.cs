using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusion
{
    internal class City
    {
        private bool _checked = false;

        public string Country { get; }

        public int X { get; }

        public int Y { get; } 

        internal Dictionary<string, int> Coins { get; }

        private readonly Dictionary<string, int> _difference = new Dictionary<string, int>();

        public City(string country, int x, int y)
        {
            Country = country;
            X = x;
            Y = y;

            Coins = new Dictionary<string, int>
            {
                { country, 1000000 }
            };
        }

        public void AddIncoming(Dictionary<string, int> incoming)
        {
            foreach (var pair in incoming)
            {
                if (_difference.ContainsKey(pair.Key))
                {
                    _difference[pair.Key] += pair.Value;
                } 
                else
                {
                    _difference.Add(pair.Key, pair.Value);
                }
            }
        }

        public Dictionary<string, int> GetOutcoming()
        {
            var dict = new Dictionary<string, int>();

            foreach (var pair in Coins)
            {
                dict.Add(pair.Key, pair.Value / 1000);

                if (_difference.ContainsKey(pair.Key))
                {
                    _difference[pair.Key] -= dict[pair.Key];
                }
                else
                {
                    _difference.Add(pair.Key, -dict[pair.Key]);
                }
            }

            return dict;
        }

        public void ApplyIncoming()
        {
            foreach (var pair in _difference)
            {
                if (Coins.ContainsKey(pair.Key))
                {
                    Coins[pair.Key] += pair.Value;
                }
                else
                {
                    Coins.Add(pair.Key, pair.Value);
                }
            }

            _difference.Clear();
        }
    }
}
