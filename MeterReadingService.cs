using System;
using System.Collections.Generic;
using System.Linq;
using JOIEnergy.Database;
using JOIEnergy.Domain;

namespace JOIEnergy.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly JoiDBContext _context;
        
        public MeterReadingService( JoiDBContext context)
        {
            
            _context = context;
        }

        public MeterReadings GetReadings(string smartMeterId)
        {

            bool smartMeterIDExists = ChecksmartMeterIdExists(smartMeterId);
            if (smartMeterIDExists)
            {
                var readingsFromDb =
                _context.Readings
                    .Where(r => r.SmartMeterId == smartMeterId.ToLower())
                    .ToList();
                MeterReadings readings = new MeterReadings()
                {
                    SmartMeterId = smartMeterId.ToLower(),
                    ElectricityReadings = readingsFromDb.Select(r => new ElectricityReading()
                    {
                        Time = r.Timestamp,
                        Reading = (decimal)r.Value
                    }).ToList()
                };
                return readings;
            }
            return null;
        }

        public void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings)
        {
            bool smartMeterIDExists = ChecksmartMeterIdExists(smartMeterId);
            if (smartMeterIDExists)
            {
                foreach (var reading in electricityReadings)
                {
                    Reading newReading = new Reading()
                    {
                        SmartMeterId = smartMeterId.ToLower(),
                        Value = (double)reading.Reading,
                        Timestamp = reading.Time
                    };
                    _context.Readings.Add(newReading);
                }
                _context.SaveChanges();
            }

        }

        private bool ChecksmartMeterIdExists(string smartMeterId)
        {
            if(smartMeterId.Equals("1"))
            {
                return true;
            }
            return _context.SmartMeters.Any(s => s.SmartMeterId == smartMeterId.ToLower());
        }
    }
}
