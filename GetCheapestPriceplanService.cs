using JOIEnergy.Domain;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace JOIEnergy.Services
{
    public class GetCheapestPriceplanService : IGetCheapestPriceplanService
    {
        private readonly IMeterReadingService _meterReadingService;
        private IPricePlanService _priceplanService;
        private readonly List<PricePlan> _pricePlans;
        private readonly IDatabaseConnection _databaseConnection;

        public GetCheapestPriceplanService(IMeterReadingService meterReadingService, IPricePlanService priceplanService, List<PricePlan> _pricePlans, IDatabaseConnection databaseConnection)
        {
            _meterReadingService = meterReadingService;
            _priceplanService = priceplanService;
            _pricePlans = _pricePlans;
            _databaseConnection = databaseConnection;
        }

        public Dictionary<string, decimal> CheapestPriceplan(string smartMeterId, List<PricePlan> _pricePlans)
        {
            Dictionary<string, decimal> cheapestPriceplan;
            Dictionary<string, decimal> cheapplan;

            cheapestPriceplan = _priceplanService.GetConsumptionCostOfElectricityReadingsForEachPricePlan(smartMeterId);
            List<decimal> prices = cheapestPriceplan.Values.ToList();
            cheapplan = cheapestPriceplan.Where(x => x.Value == prices.Min()).ToDictionary(x => x.Key, x => x.Value);
            return cheapplan;

        }

    }
}
