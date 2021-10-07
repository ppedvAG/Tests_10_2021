using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Calculator.Tests
{
    [TestClass]
    public class CalcTests
    {
        [TestMethod]
        public void Sum_2_and_3_results_5()
        {
            //Arrange
            Calc calc = new Calc();

            //Act
            var result = calc.Sum(2, 3);

            //Assert
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Sum_0_and_0_results_0()
        {
            //Arrange
            Calc calc = new Calc();

            //Act
            var result = calc.Sum(0, 0);

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        [TestCategory("ExceptionTest")]
        public void Sum_MAX_and_1_throws_OverflowException()
        {
            Calc calc = new Calc();

            Assert.ThrowsException<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }

        [TestMethod]
        [DataRow(0, 0, 0)]
        [DataRow(1, 2, 3)]
        [DataRow(-3, -2, -5)]
        [DataRow(-3, 5, 2)]
        public void Sum_DataRows(int a, int b, int expected)
        {
            Calc calc = new Calc();

            var result = calc.Sum(a, b);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Calc_Minus()
        {
            Calc calc = new Calc();

            var result = calc.Minus(10, 4);

            Assert.AreEqual(6, result);
        }
    }
}