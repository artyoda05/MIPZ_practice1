using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusion.Tests
{
    internal class EuroDiffusionTestCases : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new TestCase
                {
                    Countries = new List<Country>
                    {
                        new Country
                        {
                            Name = "Luxembourg",
                            Xl = 1,
                            Yl = 1,
                            Xr = 1,
                            Yr = 1
                        }
                    }
                },
                new TestCaseResult
                {
                    Result = new Dictionary<string, int>
                    {
                        { "Luxembourg", 0 }
                    }
                }
            };

            yield return new object[]
            {
                new TestCase
                {
                    Countries = new List<Country>
                    {
                        new Country
                        {
                            Name = "Netherlands",
                            Xl = 1,
                            Yl = 3,
                            Xr = 2,
                            Yr = 4
                        },
                        new Country
                        {
                            Name = "Belgium",
                            Xl = 1,
                            Yl = 1,
                            Xr = 2,
                            Yr = 2
                        }
                    }
                },
                new TestCaseResult
                {
                    Result = new Dictionary<string, int>
                    {
                        { "Belgium", 2 },
                        { "Netherlands", 2 },
                    }
                }
            };

            yield return new object[]
            {
                new TestCase
                {
                    Countries = new List<Country>
                    {
                        new Country
                        {
                            Name = "France",
                            Xl = 1,
                            Yl = 4,
                            Xr = 4,
                            Yr = 6
                        },
                        new Country
                        {
                            Name = "Spain",
                            Xl = 3,
                            Yl = 1,
                            Xr = 6,
                            Yr = 3
                        },
                        new Country
                        {
                            Name = "Portugal",
                            Xl = 1,
                            Yl = 1,
                            Xr = 2,
                            Yr = 2
                        }
                    }
                },
                new TestCaseResult
                {
                    Result = new Dictionary<string, int>
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
}
