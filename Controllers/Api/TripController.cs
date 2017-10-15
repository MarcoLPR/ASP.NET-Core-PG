using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_Core_PG.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Unosquare.Tubular;
using Unosquare.Tubular.ObjectModel;

namespace ASP.NET_Core_PG.Controllers.Api
{
    [Route("api/trips")]
    public class TripController : Controller
    {
        private ITravelRepository _repository;
        private ILogger<TripController> _logger;
        private TravelContext _context;

        public TripController(ITravelRepository repository, ILogger<TripController> logger, TravelContext context)
        {
            _context = context;
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetAllTrips();

                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all trips: {ex}");

                return BadRequest("Bad Get Request");
            }
        }

        [HttpPost("paged")]
        public IActionResult GridData([FromBody] GridDataRequest request)
        {
            return Ok(request.CreateGridDataResponse(_context.Trips));
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel theTrip)
        {
            if (ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(theTrip);
                _repository.AddTrip(newTrip);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/trip/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
            }

            return BadRequest("Failed to Save Changes");
        }

        /*[HttpPut]
        public IActionResult PutData([FromBody] GridDataUpdateRow<TripViewModel> theTrip)
        {
            var newTrip = _repository.GetData().FirstOrDefault(x => x.Id == model.Old.Id);

            if (item == null)
                return NotFound();

            item.Name = model.New.Name;
            item.Amount = model.New.Amount;
            item.Date = model.New.Date;

            return Ok();
        }

        [HttpGet, Route("select/{id}")]
        public IActionResult Get(int id)
        {
            var item = _repository.GetData().FirstOrDefault(x => x.Id == id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpDelete, Route("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _repository.GetData().FirstOrDefault(x => x.Id == id);

            if (item == null)
                return NotFound();

            _repository.RemoveItem(item);

            return Ok();
        }*/
    }
}