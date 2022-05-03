using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusion
{
    internal class TestCaseResult
    {
        public Dictionary<string, int> Result { get; set; } = new Dictionary<string, int>();

        public void OutputToConsole()
        {
            foreach (var result in Result)
            {
                Console.WriteLine($"{result.Key} {result.Value}");
            }
        }
    }
}
