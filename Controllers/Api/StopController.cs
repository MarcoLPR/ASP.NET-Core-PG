using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_Core_PG.Models;
using Microsoft.Extensions.Logging;
using AutoMapper;
using ASP.NET_Core_PG.Services;
using Unosquare.Tubular.ObjectModel;
using Unosquare.Tubular;

namespace ASP.NET_Core_PG.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private TravelContext _context;
        private ITravelRepository _repository;
        private ILogger<StopController> _logger;

        public StopController(ITravelRepository repository, ILogger<StopController> logger, TravelContext context)
        {
            _context = context;
            _repository = repository;
            _logger = logger;
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

        [HttpPost("paged")]
        public IActionResult GridData([FromBody] GridDataRequest request)
        {
            return Ok(request.CreateGridDataResponse(_context.Stops));
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
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stop = _repository.GetStopById(id);
            if (stop == null)
            {
                return NotFound();
            }
            _repository.DeleteStop(stop);

            if (await _repository.SaveChangesAsync())
            {
                return Ok("Stop Deleted");
            }

            return BadRequest("Failed to Delete Stop");
        }
    }
}