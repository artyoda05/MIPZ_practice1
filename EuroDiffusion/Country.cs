using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusion
{
    internal class Country
    {
        public string Name { get; set; }

        public int Xl { get; set; }

        public int Yl { get; set; }

        public int Xr { get; set; }

        public int Yr { get; set; }

        public static Country InputFromConsole()
        {
            var input = Console.ReadLine();

            var splited = input?.Split(' ');

            if (splited == null 
                || splited.Length != 5 
                || splited[0].Length > 25 
                || !int.TryParse(splited[1], out var Xl)
                || !int.TryParse(splited[2], out var Yl)
                || !int.TryParse(splited[3], out var Xr)
                || !int.TryParse(splited[4], out var Yr))
            {
                throw new Exception();
            }

            return new Country
            {
                Name = splited[0],
                Xl = Xl,
                Yl = Yl,
                Xr = Xr,
                Yr = Yr
            };
        }

        public List<City> Cities {
            get
            {
                var cities = new List<City>();

                for (var i = Xl; i <= Xr; i++)
                {
                    for (var j = Yl; j <= Yr; j++)
                    {
                        cities.Add(new City(Name, i, j));
                    }
                }

                return cities;
            } 
        }
    }
}
