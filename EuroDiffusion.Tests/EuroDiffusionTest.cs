using Xunit;
using EuroDiffusion.Entities;

namespace EuroDiffusion.Tests;

public class EuroDiffusionTest
{
    [Theory]
    [ClassData(typeof(EuroDiffusionTestCases))]
    internal void Test(TestCase testCase, TestCaseResult expected)
    {
        var actual = testCase.Execute();

        Assert.Equal(expected.Result, actual.Result);
    }
}
