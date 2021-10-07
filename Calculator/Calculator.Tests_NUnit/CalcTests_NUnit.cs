using NUnit.Framework;
using System;

namespace Calculator.Tests_NUnit
{
    [TestFixture]
    public class Tests
    {

        [Test]
        public void Sum_5_and_6_results_11()
        {
            Calc calc = new Calc();

            var result = calc.Sum(5, 6);

            Assert.AreEqual(11, result);
            Assert.That(result, Is.EqualTo(11));
        }

        [Test]
        [Category("ExceptionTest")]
        public void Sum_MIN_and_n1_throws_OverflowException()
        {
            Calc calc = new Calc();

            Assert.Throws<OverflowException>(() => calc.Sum(int.MinValue, -1));
        }

        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(1, 2, 3)]
        [TestCase(-3, -2, -5)]
        [TestCase(-3, 5, 2)]
        public void Sum_TestCases(int a, int b, int expected)
        {
            Calc calc = new Calc();

            var result = calc.Sum(a, b);

            Assert.AreEqual(expected, result);
        }
    }
}