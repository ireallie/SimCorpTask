using System;

namespace SimCorpTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var investmentInfo = new InvestmentCalculation();
            investmentInfo.CalcPayments();
            Console.WriteLine(investmentInfo);

            Console.ReadLine();
        }  
    }
}
