using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}