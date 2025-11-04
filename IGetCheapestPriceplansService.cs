using JOIEnergy.Domain;
using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace JOIEnergy.Services
{
    public interface IGetCheapestPriceplanService
    {
        public Dictionary<string, decimal> CheapestPriceplan(string smartMeterId, List<PricePlan> pricePlan);
//        public Dictionary<string, decimal> cheapestPriceplan;
    }
}
