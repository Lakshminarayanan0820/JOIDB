using System;
using System.Collections.Generic;
using System.Linq;
using JOIEnergy.Domain;

namespace JOIEnergy.Services
{
    public class PricePlanService : IPricePlanService
    {
        public interface Debug { void Log(string s); };

        private readonly List<PricePlan> _pricePlans;
        private IMeterReadingService _meterReadingService;

        public PricePlanService(List<PricePlan> pricePlan, IMeterReadingService meterReadingService)
        {
            _pricePlans = pricePlan;
            _meterReadingService = meterReadingService;
        }

        private decimal calculateAverageReading(List<ElectricityReading> electricityReadings)
        {
            var newSummedReadings = electricityReadings.Select(readings => readings.Reading).Aggregate((reading, accumulator) => reading + accumulator);

            return newSummedReadings / electricityReadings.Count();
        }

        private decimal calculateTimeElapsed(List<ElectricityReading> electricityReadings)
        {
            var first = electricityReadings.Min(reading => reading.Time);
            var last = electricityReadings.Max(reading => reading.Time);

            return (decimal)(last - first).TotalHours;
        }
        private decimal calculateCost(List<ElectricityReading> electricityReadings, PricePlan pricePlan)
        {
            var average = calculateAverageReading(electricityReadings);
            var timeElapsed = calculateTimeElapsed(electricityReadings);
            var averagedCost = average / timeElapsed;
            return Math.Round(averagedCost * pricePlan.UnitRate, 3);
        }

        public Dictionary<string, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(string smartMeterId)
        {
            throw new NotImplementedException();
 //           List<ElectricityReading> electricityReadings = _meterReadingService.GetReadings(smartMeterId);

 //           if (!electricityReadings.Any())
 //           {
 //               return new Dictionary<string, decimal>();
 //           }
 //           var planNames = _pricePlans.Select(plan => plan.PlanName);//planNames

 ////           var planeNamesStartinggWithS = planNames.Where(planName => planName.StartsWith('S') );
 //           return _pricePlans.ToDictionary(plan => plan.PlanName, plan => calculateCost(electricityReadings, plan));
        }
    }
}
