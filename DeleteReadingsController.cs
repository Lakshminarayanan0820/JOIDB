using JOIEnergy.Domain;
using JOIEnergy.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JOIEnergy.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("delete-readings")]

    public class DeleteReadingsController : Controller
    {
        private readonly IDeleteReadingsForGivenSmartmeterIDService _deleteReadingsForGivenSmartmeterIDService;
        private readonly IMeterReadingService _meterReadingService;

 //       private readonly IServiceProvider _serviceProvider;
        public Dictionary<string, List<ElectricityReading>> MeterAssociatedReadings { get; set; }


        public DeleteReadingsController(IDeleteReadingsForGivenSmartmeterIDService deleteReadingsForGivenSmartmeterIDService, IMeterReadingService meterReadingService, Dictionary<string, List<ElectricityReading>> meterAssociatedReadings)
        {
            _deleteReadingsForGivenSmartmeterIDService = deleteReadingsForGivenSmartmeterIDService;
            _meterReadingService = meterReadingService;
            //           _serviceProvider = serviceProvider;
            MeterAssociatedReadings = meterAssociatedReadings;
        }

        [HttpDelete("delete/{smartMeterId}/{DeleteCount}")]
        public async Task<IActionResult>  DeleteReadings(string smartMeterId, int DeleteCount)
        {
 //           List<ElectricityReading> readings = _meterReadingService.GetReadings(smartMeterId);
 //          readings = _meterReadingService.GetReadings(smartMeterId);
 //           DeleteReadings _deleteReadings = new DeleteReadings();
 //           if(DeleteCount > MeterAssociatedReadings[smartMeterId].Count())
 //           {
 //               return new BadRequestObjectResult("Given count is greater than available Readings ");
 //           }
            var read =  _meterReadingService.GetReadings(smartMeterId);
            if (MeterAssociatedReadings.TryGetValue(smartMeterId , out _))
            { 
                bool IsDeleted = _deleteReadingsForGivenSmartmeterIDService.DeleteReadingsForGivenSmartmeterID(smartMeterId, DeleteCount);
                if (IsDeleted)
                {
                    return NoContent();
                }
            }

            return BadRequest("No readings available");            
        }
    }
      

    }
