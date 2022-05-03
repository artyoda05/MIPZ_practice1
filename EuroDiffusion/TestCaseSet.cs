using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusion
{
    internal class TestCaseSet
    {
        public List<TestCase> TestCases { get; set; } = new List<TestCase>();
        public List<TestCaseResult> Results { get; set; } = new List<TestCaseResult>();

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

        public void OutputToConsole()
        {
            for (var i = 0; i < Results.Count; i++)
            {
                Console.WriteLine($"Case Number {i+1}");

                Results[i].OutputToConsole();
            }
        }
    }
}
