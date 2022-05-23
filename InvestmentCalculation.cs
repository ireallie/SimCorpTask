using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimCorpTask
{
    public class InvestmentCalculation
    {
        public DateTime AgreementDate { get; set; }
        public DateTime CalculationDate { get; set; }
        public decimal InvestmentSum { get; set; }
        public decimal InterestRate { get; set; }
        public int InvestmentDuration { get; set; }
        public DataTable InvestmentDetailsTable { get; set; }
        public DateTime EndContractDate { get; set; }
        public decimal TotalInterest { get; set; }

        /// <summary>
        /// Method that calculates the interest amount in the monthly payment.
        /// </summary>
        /// <param name="investmentSum"></param>
        /// <param name="interestRate"></param>
        /// <returns></returns>
        public static decimal CalcInterestAmount(decimal investmentSum, decimal interestRate) 
        {
            return investmentSum * interestRate;    
        }

        /// <summary>
        /// Method that calculates share of interest rate (per month).
        /// </summary>
        /// <param name="interestRate"></param>
        /// <returns></returns>
        public static decimal CalcMonthlyInterestRate(decimal interestRate)
        {
            return interestRate / 100 / 12;
        }

        /// <summary>
        /// Method that calculates the amount of the monthly payment.
        /// </summary>
        /// <param name="investmentSum"></param>
        /// <param name="interestAmount"></param>
        /// <param name="investmentDuration"></param>
        /// <returns></returns>
        public static decimal CalcPaymentAmount(decimal investmentSum, decimal interestAmount, int investmentDuration)
        {
            int months = CalcMonthCount(investmentDuration);

            return investmentSum * (interestAmount + (interestAmount / (decimal)(Math.Pow((double)(1 + interestAmount), months) - 1)));
        }

        /// <summary>
        /// Method that counts the number of months in a year.
        /// </summary>
        /// <param name="investmentDuration"></param>
        /// <returns></returns>
        private static int CalcMonthCount(int investmentDuration)
        {
            return investmentDuration * 12;
        }

        public InvestmentCalculation()
        {
            Console.WriteLine("Enter an Agreement date (MM/dd/yyyy):");
            AgreementDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Enter a Calculation date (MM/dd/yyyy):");
            CalculationDate = DateTime.Parse(Console.ReadLine());   

            Console.WriteLine("Enter an Investment sum (X $):");
            InvestmentSum = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Enter an Interest rate (R %):");
            InterestRate = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Enter an Investment duration (N years):");
            InvestmentDuration = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            InvestmentDetailsTable = new DataTable("Investment Details");
            DataColumn[] cols ={
                                  new DataColumn("Period #",typeof(Int32)),
                                  new DataColumn("Payment Amount",typeof(Decimal)),
                                  new DataColumn("Principal Amount",typeof(Decimal)),
                                  new DataColumn("Interest Amount",typeof(Decimal)),
                                  new DataColumn("Balance Owed",typeof(Decimal)),
                              };

            InvestmentDetailsTable.Columns.AddRange(cols);
        }

        /// <summary>
        /// Method that calculates all payments from Calculation date.
        /// </summary>
        public void CalcPayments()
        {
            decimal monthlyInterestRate = CalcMonthlyInterestRate(InterestRate);
            decimal paymentAmount = CalcPaymentAmount(InvestmentSum, monthlyInterestRate, InvestmentDuration);

            EndContractDate = AgreementDate.AddYears(InvestmentDuration);
            decimal investmentSumLeft = InvestmentSum;
            int period = 1;

            CalcIntermidiateValues(AgreementDate, CalculationDate, ref paymentAmount, ref investmentSumLeft, ref monthlyInterestRate, ref period);
            CalcIntermidiateValues(CalculationDate, EndContractDate, ref paymentAmount, ref investmentSumLeft, ref monthlyInterestRate, ref period);
        }

        /// <summary>
        /// Helper method that calculates intermidiate values for calculating all payments.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="paymentAmount"></param>
        /// <param name="investmentSumLeft"></param>
        /// <param name="monthlyInterestRate"></param>
        /// <param name="period"></param>
        private void CalcIntermidiateValues(DateTime startDate, DateTime endDate, ref decimal paymentAmount, ref decimal investmentSumLeft, ref decimal monthlyInterestRate, ref int period)
        {
            while (startDate.Date < endDate.Date)
            {
                decimal interestAmount = CalcInterestAmount(investmentSumLeft, monthlyInterestRate);
                decimal principalAmount = paymentAmount - interestAmount;
                decimal balanceOwed = investmentSumLeft - principalAmount;

                if (endDate == EndContractDate)
                    InvestmentDetailsTable.Rows.Add(period, Math.Round(paymentAmount, 2), Math.Round(principalAmount, 2), Math.Round(interestAmount, 2), Math.Round(balanceOwed, 2));

                investmentSumLeft -= principalAmount;
                startDate = startDate.AddMonths(1);
                period++;
                TotalInterest += interestAmount;
            }
        }

        public override string ToString()
        {
            string result = string.Empty;

            result += $"Total Payments: {Math.Round(InvestmentSum + TotalInterest, 2)}\n";
            result += $"Total Interest: {Math.Round(TotalInterest, 2)}\n\n";

            result += string.Join(Environment.NewLine,
            InvestmentDetailsTable.Rows.OfType<DataRow>().Select(x => string.Join(" ", x.ItemArray)));

            return result;
        }

    }
}
