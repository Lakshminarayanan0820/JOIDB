using System.Collections.Generic;
using JOIEnergy.Domain;

namespace JOIEnergy.Services
{
    public interface IMeterReadingService
    {
        MeterReadings GetReadings(string smartMeterId);
        void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings);
    }
}