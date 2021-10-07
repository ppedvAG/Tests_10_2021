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
    }
}
