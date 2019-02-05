using System;
using Xunit;
using Xunit.Abstractions;

namespace DEXS.Tools.Extensions.DateTime.Tests
{
    public class DateTest
    {
        public System.DateTime Date { get; set; }
        public System.DateTime Expected { get; set; }
        public string Offset { get; set; }
    }

    public class CalculationTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public CalculationTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            // Set the Default Direction to forward
            DateExtensions.DefaultDirection = "+";
        }

        // f, F
        // s, S
        // m
        // h, H
        // d, D
        // w, W
        // M
        // y, Y

        public static object[][] TestData =
        {
            new object[] { new System.DateTime(2018, 1, 1), new System.DateTime(2019, 1, 1), "1y" },
            new object[] { new System.DateTime(2018, 1, 1), new System.DateTime(2019, 1, 1), "1Y" },
            new object[] { new System.DateTime(2018, 1, 1), new System.DateTime(2018, 2, 1), "1M" },
            new object[] { new System.DateTime(2018, 1, 1), new System.DateTime(2018, 1, 8), "1w" },
            new object[] { new System.DateTime(2018, 1, 1), new System.DateTime(2018, 1, 8), "1W" },
            new object[] { new System.DateTime(2018, 1, 1), new System.DateTime(2018, 1, 2), "1d" },
            new object[] { new System.DateTime(2018, 1, 1), new System.DateTime(2018, 1, 2), "1D" },
            new object[] { new System.DateTime(2018, 1, 1, 0, 0, 0), new System.DateTime(2018, 1, 1, 1, 0, 0), "1h" },
            new object[] { new System.DateTime(2018, 1, 1, 0, 0, 0), new System.DateTime(2018, 1, 1, 1, 0, 0), "1H" },
            new object[] { new System.DateTime(2018, 1, 1, 0, 0, 0), new System.DateTime(2018, 1, 1, 0, 1, 0), "1m" },
            new object[] { new System.DateTime(2018, 1, 1, 0, 0, 0), new System.DateTime(2018, 1, 1, 0, 0, 1), "1s" },
            new object[] { new System.DateTime(2018, 1, 1, 0, 0, 0), new System.DateTime(2018, 1, 1, 0, 0, 1), "1S" },
            new object[] { new System.DateTime(2018, 1, 1, 0, 0, 0, 0), new System.DateTime(2018, 1, 1, 0, 0, 0, 1), "1f" },
            new object[] { new System.DateTime(2018, 1, 1, 0, 0, 0, 0), new System.DateTime(2018, 1, 1, 0, 0, 0, 1), "1F" },

            new object[] { new System.DateTime(2018, 1, 1), new System.DateTime(2017, 1, 1), "-1y" },
            new object[] { new System.DateTime(2018, 1, 1), new System.DateTime(2017, 12, 1), "-1M" },
            new object[] { new System.DateTime(2018, 1, 1), new System.DateTime(2017, 12, 25), "-1w" },
            new object[] { new System.DateTime(2018, 1, 1), new System.DateTime(2017, 12, 31), "-1d" },
            new object[] { new System.DateTime(2018, 1, 1, 0, 0, 0), new System.DateTime(2017, 12, 31, 23, 0, 0), "-1h" },
            new object[] { new System.DateTime(2018, 1, 1, 0, 0, 0), new System.DateTime(2017, 12, 31, 23, 59, 0), "-1m" },
            new object[] { new System.DateTime(2018, 1, 1, 0, 0, 0), new System.DateTime(2017, 12, 31, 23, 59, 59), "-1s" },
            new object[] { new System.DateTime(2018, 1, 1, 0, 0, 0, 0), new System.DateTime(2017, 12, 31, 23, 59, 59, 999), "-1f" }
        };

        public static object[][] BadTestData =
        {
            new object[] { "" },
            new object[] { "M" },
            new object[] { "1" },
            new object[] { "1j" }, 
        };

        public static object[][] LookbackStringTests =
        {
            new object[] { "1y", "1 Year" },
            new object[] { "2y", "2 Years" },
            new object[] { "-1y", "-1 Year" },
            new object[] { "0y", "0 Years" },
            new object[] { "1M2m3s", "1 Month 2 Minutes 3 Seconds" },
        };

        [Fact]
        public void CanApplyOffset()
        {
            var date = new System.DateTime(2018, 1, 1);
            var expected = new System.DateTime(2018, 2, 1);
            var result = date.ApplyOffset("M", 1);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void FailsWithArgumentNullExceptionWhenOffsetIsNull()
        {
            var date = new System.DateTime(2018, 1, 1);
            Assert.Throws<ArgumentNullException>(() =>
            {
                date.Calculate(null);
            });
        }

        [Theory, MemberData(nameof(BadTestData))]
        public void FailsIfOffsetIsInvalid(string offset)
        {
            var date = new System.DateTime(2018, 1, 1);
            Assert.Throws<ArgumentException>(() =>
            {
                date.Calculate(offset);
            });
        }

        [Theory, MemberData(nameof(TestData))]
        public void CanCalculateOffsets(System.DateTime date, System.DateTime expected, string offset)
        {
            var result = date.Calculate(offset);
            Assert.Equal(expected, result);
        }

        [Theory, MemberData(nameof(LookbackStringTests))]
        public void CanGetHumanReadable(string lookback, string expectation)
        {
            var result = lookback.ToLookbackDictionary().ToHumanReadable();
            Assert.Equal(expectation, result);
            _testOutputHelper.WriteLine(result);
        }
    }
}
