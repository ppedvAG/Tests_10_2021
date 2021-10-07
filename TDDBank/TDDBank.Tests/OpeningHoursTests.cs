using Microsoft.QualityTools.Testing.Fakes;

using System;
using Xunit;

namespace TDDBank.Tests
{
    public class OpeningHoursTests
    {
        [Theory]
        [InlineData(2021, 10, 4, 10, 30, true)]//mo
        [InlineData(2021, 10, 4, 10, 29, false)]//mo
        [InlineData(2021, 10, 4, 10, 0, false)] //mo
        [InlineData(2021, 10, 4, 18, 59, true)] //mo
        [InlineData(2021, 10, 4, 19, 00, false)] //mo
        [InlineData(2021, 10, 9, 13, 0, true)] //sa
        [InlineData(2021, 10, 9, 16, 0, false)] //sa
        [InlineData(2021, 10, 10, 20, 0, false)] //so
        public void OpeningHours_IsOpen(int y, int M, int d, int h, int m, bool result)
        {
            var dt = new DateTime(y, M, d, h, m, 0);
            var oh = new OpeningHours();

            Assert.Equal(result, oh.IsOpen(dt));
        }

        [Fact]
        public void IsWeekend()
        {
            var oh = new OpeningHours();

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 10, 4, 12, 30, 0);
                Assert.False(oh.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 10, 5, 12, 30, 0);
                Assert.False(oh.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 10, 6, 12, 30, 0);
                Assert.False(oh.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 10, 7, 12, 30, 0);
                Assert.False(oh.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 10, 8, 12, 30, 0);
                Assert.False(oh.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 10, 9, 12, 30, 0);
                Assert.True(oh.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 10, 10, 12, 30, 0);
                Assert.True(oh.IsWeekend());
            }

        }

        [Fact]
        public void GetInfoFromFile()
        {
            var oh = new OpeningHours();

            using (ShimsContext.Create())
            {
                System.IO.Fakes.ShimFile.ReadAllTextString = fileName => "Bier";
                Assert.Equal("Bier", oh.GetInfoFromFile());
            }
        }
    }
}
