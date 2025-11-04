using JOIEnergy.Domain;
using System.Collections.Generic;

namespace JOIEnergy.Services
{
    public interface IDeleteReadingsForGivenSmartmeterIDService
    {
        public bool DeleteReadingsForGivenSmartmeterID(string smartMeterId, int DeleteCount);
        public List<ElectricityReading> RemoveReadings(int DelCount, Dictionary<string, List<ElectricityReading>> Readings, string smartMeterId);
    }
}
