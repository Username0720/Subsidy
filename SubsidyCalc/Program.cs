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
                subsidyCalculation.OnNotify += new EventHandler<string>(NewNotify);
                charge = subsidyCalculation.CalculateSubsidy(volume, tariff);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine(charge.HouseId);
            Console.WriteLine(charge.Value);
            subsidyCalculation.OnNotify += new EventHandler<string>(NewNotify);
        }
        static void NewNotify(object sender, string message)
        {
            Console.WriteLine(message);
        }
    }
}
