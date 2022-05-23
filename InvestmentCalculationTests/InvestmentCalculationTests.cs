using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimCorpTask;

namespace InvestmentCalculationTests
{
    [TestClass]
    public class InvestmentCalculationTests
    {
        [TestMethod]
        public void CalcInterestAmount_WithValidInterestAmount()
        {
            // Arrange
            decimal investmentSum = 200000;
            decimal interestRate = (decimal)0.01;
            decimal expected = 2000;
            decimal actual = 0;

            // Act
            actual = InvestmentCalculation.CalcInterestAmount(investmentSum, interestRate);

            // Assert
            Assert.AreEqual(expected, actual, "Interest amount per month is not correct.");
        }

        [TestMethod]
        public void CalcMonthlyInterestRate_WithValidInterestAmount()
        {
            // Arrange
            decimal annualInterestRate = 12;
            decimal expected = (decimal)0.01;
            decimal actual = 0;

            // Act
            actual = InvestmentCalculation.CalcMonthlyInterestRate(annualInterestRate);

            // Assert
            Assert.AreEqual(expected, actual, "Montly interest rate is not correct.");
        }
    }
}
