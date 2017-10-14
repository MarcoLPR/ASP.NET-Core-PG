using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_Core_PG.Models;
using Microsoft.Extensions.Logging;
using AutoMapper;
using ASP.NET_Core_PG.Services;

namespace ASP.NET_Core_PG.Controllers.Api
{
    [Route("/api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private ITravelRepository _repository;
        private ILogger<StopController> _logger;
        private GeoCoordsService _coordsService;

        public StopController(ITravelRepository repository, ILogger<StopController> logger, GeoCoordsService coordsService)
        {
            _repository = repository;
            _logger = logger;
            _coordsService = coordsService;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try {
            var trip = _repository.GetTripByName(tripName);

            return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order).ToList()));
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to get stops: {0}", ex);
            }

            return BadRequest("Failed to get stops");
        }

        [HttpPost("")]
        public async Task<IActionResult> PostAsync(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(vm);
                    _repository.AddStop(tripName, newStop);

                    if (await _repository.SaveChangesAsync())
                        {
                            return Created($"/api/trips/{tripName}/stops/{newStop.Name}",
                                Mapper.Map<StopViewModel>(newStop));
                        }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Failed to save new Stop: {0}", ex);
            }
            return BadRequest("Failed to save new stop");
        }
    }
}