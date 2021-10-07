using System;
using Xunit;

namespace Calculator.Tests_xUnit
{
    public class CalcTests_xUnit
    {
        [Fact]
        public void Sum_2_and_3_results_5()
        {
            //Arrange
            Calc calc = new Calc();

            //Act
            var result = calc.Sum(2, 3);

            //Assert
            Assert.Equal(5, result);
        }

        [Fact]
        [Trait("","ExceptionTest")]
        public void Sum_MAX_and_1_throws_OverflowException()
        {
            Calc calc = new Calc();

            Assert.Throws<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }


        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 2, 3)]
        [InlineData(-3, -2, -5)]
        [InlineData(-3, 5, 2)]
        public void Sum_DataRows(int a, int b, int expected)
        {
            Calc calc = new Calc();

            var result = calc.Sum(a, b);

            Assert.Equal(expected, result);
        }
    }
}