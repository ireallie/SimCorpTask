using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimCorpTask
{
    public static class InvestmentCalculation
    {
        /// <summary>
        /// A function that calculates mountly payment that doesn't change each month and represents the sum of two components: part of initial principal and interest amount.
        /// </summary>
        /// <param name="investmentDuration"></param>
        /// <param name="investmentSum"></param>
        /// <param name="interestRate"></param>
        /// <returns></returns>
        public static double CalcMonthlyPayment(int investmentDuration, double investmentSum, double interestRate)
        {
            int monthsCount = CalcMonthsCount(investmentDuration);

            return investmentSum / monthsCount + (investmentSum * (interestRate / 100)) / monthsCount;
        }

        /// <summary>
        /// A function that calculates the number of months.
        /// </summary>
        /// <param name="investmentDuration"></param>
        /// <returns></returns>
        private static int CalcMonthsCount(int investmentDuration)  
        {
            return investmentDuration * 12;
        }

        /// <summary>
        /// A function that calculates monthly payment from the beginning of the month to calculation date.
        /// </summary>
        /// <param name="calculationDate"></param>
        /// <param name="investmentSum"></param>
        /// <param name="interestRate"></param>
        /// <returns></returns>
        public static double CalcMonthlyPaymentToCalcDate(DateTime calculationDate, double investmentSum, double interestRate)
        {
            var firstDayInMonth = new DateTime(calculationDate.Year, calculationDate.Month, 1);
            int daystoCalcDate = (calculationDate - firstDayInMonth).Days;

            return investmentSum / daystoCalcDate + (investmentSum * (interestRate / 100)) / daystoCalcDate;
        }

        /// <summary>
        /// A function that calculates sum of all future interest payments.
        /// </summary>
        /// <param name="investmentSum"></param>
        /// <param name="interestRate"></param>
        /// <returns></returns>
        public static double CalcTotalSum(double investmentSum, double interestRate)
        {
            return investmentSum + (investmentSum * (interestRate / 100));
        }

        /// <summary>
        /// A function that calculates sum of from agreement date to calculation date.
        /// </summary>
        /// <param name="calculationDate"></param>
        /// <param name="agreementDate"></param>
        /// <param name="investmentDuration"></param>
        /// <param name="investmentSum"></param>
        /// <param name="interestRate"></param>
        /// <returns></returns>
        public static double CalcTotalSumToCalcDate(DateTime calculationDate, DateTime agreementDate, int investmentDuration, double investmentSum, double interestRate)
        {
            int months = (calculationDate.Year - agreementDate.Year) * 12 + calculationDate.Month - agreementDate.Month;
            double monthlyPayment = CalcMonthlyPayment(investmentDuration, investmentSum, interestRate);
            double monthlyPaymentToCalcDate = CalcMonthlyPaymentToCalcDate(calculationDate, investmentSum, interestRate);

            return monthlyPayment * months + monthlyPaymentToCalcDate;
        }
    }
}
