using JOIEnergy.Domain;
using System.Collections.Generic;
using JOIEnergy.Services;
using System.Linq;

namespace JOIEnergy.Services
{
    public class DeleteReadingsForGivenSmartmeterIDService : IDeleteReadingsForGivenSmartmeterIDService
    {

        private readonly IMeterReadingService _meterReadingService;
        public Dictionary<string, List<ElectricityReading>> MeterAssociatedReadings { get; set; }
        //        private DeleteReadings _DeleteReadings;

        public DeleteReadingsForGivenSmartmeterIDService(IMeterReadingService meterReadingService, Dictionary<string, List<ElectricityReading>> meterAssociatedReadings)
        {
            _meterReadingService = meterReadingService;
            MeterAssociatedReadings = meterAssociatedReadings;
        }

        public bool DeleteReadingsForGivenSmartmeterID(string smartMeterId, int DeleteCount)
        {
            //            List<ElectricityReading> Readings = _meterReadingService.GetReadings(smartMeterId);
            //           DeleteReadings _DeleteReadings = new DeleteReadings();

            //           if (Readings.Any() == true && Readings.Count() > _DeleteReadings.DeleteCount)
            if (MeterAssociatedReadings.Any())
            {
                //               int DelCount = DeleteCount;
                var readings = RemoveReadings(DeleteCount, MeterAssociatedReadings, smartMeterId);
                if(readings == null || readings.Count == 0)
                {
                    return false; // No readings to delete
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ElectricityReading> RemoveReadings(int DelCount, Dictionary<string, List<ElectricityReading>> Readings, string smartMeterId)
        {
            if (MeterAssociatedReadings.TryGetValue(smartMeterId, out List<ElectricityReading> readings))
            {
                if (DelCount > readings.Count())
                {
                    DelCount = readings.Count();
                }
                for (int i = 0; i < DelCount; i++)
                {
                    readings.Remove(readings.LastOrDefault());

                    //MeterAssociatedReadings[smartMeterId].Remove(MeterAssociatedReadings[smartMeterId][MeterAssociatedReadings[smartMeterId].Count() - 1]);
                }
                return readings;
            }

            return null;
        }


    }
}
