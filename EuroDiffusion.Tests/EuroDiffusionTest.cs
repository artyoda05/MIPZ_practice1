using System;
using System.Collections.Generic;
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

        Assert.Equal(expected.Results, actual.Results);
    }

    [Fact]
    internal void TestCaseExecute_CountriesNotConnected_Throws_Exception()
    {
        var testCase = new TestCase(new List<Country>
        {
            new Country("Netherlands", 1, 3, 2, 4),
            new Country("Belgium", 1, 1, 2, 2),
            new Country("Country", 1, 6, 2, 7)
        });

        Assert.Throws<Exception>(() => testCase.Execute());
    }
}
