using System;

namespace SimCorpTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter an Agreement date (MM/dd/yyyy):");
            var agreementDate = DateTime.Parse(Console.ReadLine()).Date;

            Console.WriteLine("Enter a Calculation date (MM/dd/yyyy):");
            var calculationDate = DateTime.Parse(Console.ReadLine()).Date;

            Console.WriteLine("Enter an Investment sum (X $):");
            var investmentSum = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter an Interest rate (R %):");
            var interestRate = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter an Investment duration (N years):");
            var investmentDuration = Convert.ToInt32(Console.ReadLine());

            double monthlyPayment = InvestmentCalculation.CalcMonthlyPayment(investmentDuration, investmentSum, interestRate);
            Console.WriteLine("Monthly payment: {0}", monthlyPayment);

            double monthlyPaymentToCalcDate = InvestmentCalculation.CalcMonthlyPaymentToCalcDate(calculationDate, investmentSum, interestRate);
            Console.WriteLine("Monthly payment to Calculation date: {0}", monthlyPaymentToCalcDate);

            double totalSum = InvestmentCalculation.CalcTotalSum(investmentSum, interestRate);
            Console.WriteLine("Total sum of all future interest payments: {0}", totalSum);

            double totalSumCalcDate = InvestmentCalculation.CalcTotalSumToCalcDate(calculationDate, agreementDate, investmentDuration, investmentSum, interestRate);
            Console.WriteLine("Total sum from Agreement date to Calculation date: {0}", totalSumCalcDate);

            Console.ReadLine();
        }  
    }
}
