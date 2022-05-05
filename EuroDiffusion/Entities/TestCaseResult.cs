namespace EuroDiffusion.Entities;

internal class TestCaseResult
{
    public Dictionary<string, int> Result { get; init; } = new Dictionary<string, int>();

    public void OutputToConsole()
    {
        foreach (var result in Result)
        {
            Console.WriteLine($"{result.Key} {result.Value}");
        }
    }
}
