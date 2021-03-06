using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SubsidyCalc
{
    class SubsidyCalculation : Charge, ISubsidyCalculation
    {
        public event EventHandler<string> OnNotify;
        public event EventHandler<Tuple<string, Exception>> OnException;
        public Charge CalculateSubsidy(Volume volumes, Tariff tariff)
        {
            Charge charges = new Charge();
            try
            {
                if (volumes.HouseId != tariff.HouseId || volumes.ServiceId != tariff.ServiceId 
                    || volumes.Month.ToString() != tariff.PeriodEnd.ToString())
                {
                    OnException?.Invoke(sender: "id-шники не совпадают",
                        Tuple.Create("volumes.HouseId != tariff.HouseId", new Exception()));
                    throw new Exception("Неправильные данные");
                }
                else
                {
                    decimal v = volumes.Value;
                    decimal t = tariff.Value;
                    OnNotify?.Invoke(sender: $"Расчёт начат в {DateTime.Now}", "");
                    charges.Value = v * t;
                    Thread.Sleep(10000);
                    charges.ServiceId = volumes.ServiceId;
                    charges.HouseId = volumes.HouseId;
                    charges.Month = volumes.Month;
                }
            }
            catch (Exception e)
            {
                OnException?.Invoke(sender: "Какая-то ошибка", Tuple.Create("Неизвестная ошибка", e));
                throw;
            }
            OnNotify?.Invoke(sender: $"Расчёт успешно завершён в {DateTime.Now}", $"");
            return charges;
        }
    }
}
