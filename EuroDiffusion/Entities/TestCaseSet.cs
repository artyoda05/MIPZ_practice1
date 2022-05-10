using System.Text;

namespace EuroDiffusion.Entities;

internal class TestCaseSet
{
    public List<TestCase> TestCases { get; set; } = new List<TestCase>();

    public List<TestCaseResult> Results { get; private set; }

    public static TestCaseSet Parse(string[] parse)
    {
        var testCaseSet = new TestCaseSet();

        while (true)
        {
            if (parse.FirstOrDefault() == null 
                || !int.TryParse(parse[0], out var size)
                || size == 0)
            {
                break;
            }

            var testCase = TestCase.Parse(parse
                .Skip(1)
                .Take(size)
                .ToArray());

            testCaseSet.TestCases.Add(testCase);

            parse = parse
                .Skip(size + 1)
                .ToArray();
        }

        return testCaseSet;
    }

    public static TestCaseSet InputFromConsole()
    {
        var testCaseSet = new TestCaseSet();

        TestCase testCase;

        do
        {
            testCase = TestCase.InputFromConsole();

            testCaseSet.TestCases.Add(testCase);
        } while (testCase != null);

        return testCaseSet;
    }

    public void Execute()
    {
        Results = TestCases
            .Select(x => x.Execute())
            .ToList();
    } 

    public override string ToString()
    {
        var str = new StringBuilder();

        for (var i = 0; i < Results.Count; i++)
        {
            str.AppendLine($"Case Number {i + 1}");

            str.Append(Results[i].ToString());
        }

        return str.ToString();
    }
}
