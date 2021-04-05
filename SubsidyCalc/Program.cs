using System;

namespace SubsidyCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            Tariff tariff = new Tariff()
            {
                ServiceId = 1293,
                HouseId = 11,
                PeriodBegin = DateTime.Now.AddMonths(-2),
                PeriodEnd = DateTime.Now.AddMonths(-1),
                Value = 10
            };
            Volume volume = new Volume()
            {
                ServiceId = 1293,
                HouseId = 11,
                Month = DateTime.Now.AddMonths(-1),
                Value = 10
            };
            Charge charge = new Charge();
            SubsidyCalculation subsidyCalculation = new SubsidyCalculation();
            try
            {
                subsidyCalculation.OnNotify += NewNotify;
                charge = subsidyCalculation.CalculateSubsidy(volume, tariff);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine(charge.Value);
        }
        static void NewNotify(object sender, string message)
        {
            Console.WriteLine(sender.ToString());
        }
    }
}
