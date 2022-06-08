namespace EuroDiffusion.Entities;

internal class Country
{
    const int CountryNameLengthBoundary = 25;
    const int LowCoordinatesBoundary = 1;
    const int HighCoordinatesBoundary = 1;
    const int ExpectedAmountOfParsed = 5;

    public Country(string name, int xl, int yl, int xh, int yh)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (name.Length > CountryNameLengthBoundary)
        {
            throw new ArgumentException(nameof(name));
        }

        if (xl < LowCoordinatesBoundary || xl > HighCoordinatesBoundary)
        {
            throw new ArgumentOutOfRangeException(nameof(xl));
        }

        if (xh < xl || xh > HighCoordinatesBoundary)
        {
            throw new ArgumentOutOfRangeException(nameof(xh));
        }

        if (yl < LowCoordinatesBoundary || yl > HighCoordinatesBoundary)
        {
            throw new ArgumentOutOfRangeException(nameof(yl));
        }

        if (yh < yl || yh > HighCoordinatesBoundary)
        {
            throw new ArgumentOutOfRangeException(nameof(yh));
        }

        Name = name;
        Xl = xl;
        Yl = yl;
        Xh = xh;
        Yh = yh;
    }

    public string Name { get; }

    public int Xl { get; }

    public int Yl { get; }

    public int Xh { get; }

    public int Yh { get; }

    public List<City> Cities
    {
        get
        {
            var cities = new List<City>();

            for (var i = Xl; i <= Xh; i++)
            {
                for (var j = Yl; j <= Yh; j++)
                {
                    cities.Add(new City(Name, i, j));
                }
            }

            return cities;
        }
    }

    public static Country InputFromConsole()
    {
        var input = Console.ReadLine();

        return Parse(input);
    }

    public static Country Parse(string parse)
    {
        var splited = parse?.Split(' ');

        if (splited == null
            || splited.Length != ExpectedAmountOfParsed
            || splited[0].Length > CountryNameLengthBoundary
            || !int.TryParse(splited[1], out var Xl)
            || !int.TryParse(splited[2], out var Yl)
            || !int.TryParse(splited[3], out var Xh)
            || !int.TryParse(splited[4], out var Yh))
        {
            throw new Exception();
        }

        return new Country(splited[0], Xl, Yl, Xh, Yh);
    }
}
