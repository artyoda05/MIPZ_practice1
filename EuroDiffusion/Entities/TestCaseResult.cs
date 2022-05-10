using System.Text;

namespace EuroDiffusion.Entities;

internal class TestCaseResult
{
    public Dictionary<string, int> Results { get; init; } = new Dictionary<string, int>();

    public void OutputToConsole()
    {
        foreach (var result in Results.OrderBy(r => r.Value).ThenBy(r => r.Key))
        {
            Console.WriteLine($"{result.Key} {result.Value}");
        }
    }

    public override string ToString()
    {
        var str = new StringBuilder();

        foreach (var result in Results.OrderBy(r => r.Value).ThenBy(r => r.Key))
        {
            str.AppendLine($"{result.Key} {result.Value}");
        }

        return str.ToString();
    }
}
