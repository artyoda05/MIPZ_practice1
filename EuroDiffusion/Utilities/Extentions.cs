namespace EuroDiffusion.Utilities;

internal static class Extentions
{
    public static void AddCoins(this Dictionary<string, int> dict, string country, int amount)
    {
        if (dict.ContainsKey(country))
        {
            dict[country] += amount;
        }
        else
        {
            dict.Add(country, amount);
        }
    }
}
