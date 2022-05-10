using EuroDiffusion.Entities;
using System.Collections;
using System.Collections.Generic;

namespace EuroDiffusion.Tests;

internal class EuroDiffusionTestCases : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new TestCase(new List<Country>
                {
                    new Country("Luxembourg", 1, 1, 1, 1)
                }),
            new TestCaseResult
            {
                Results = new Dictionary<string, int>
                {
                    { "Luxembourg", 0 }
                }
            }
        };

        yield return new object[]
        {
            new TestCase(new List<Country>
                {
                    new Country("Netherlands", 1, 3 ,2, 4),
                    new Country("Belgium", 1, 1, 2, 2)                    
                }),
            new TestCaseResult
            {
                Results = new Dictionary<string, int>
                {
                    { "Belgium", 2 },
                    { "Netherlands", 2 },
                }
            }
        };

        yield return new object[]
        {
            new TestCase(new List<Country>
                {
                    new Country("France",1,4,4,6),
                    
                    new Country("Spain", 3, 1, 6, 3),
                    new Country("Portugal", 1, 1, 2, 2)                    
                }),
            new TestCaseResult
            {
                Results = new Dictionary<string, int>
                {
                    { "Spain", 382},
                    { "Portugal", 416 },
                    { "France", 1325 },
                }
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
