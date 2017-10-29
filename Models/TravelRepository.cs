using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_PG.Models
{
    public class TravelRepository : ITravelRepository
    {
        private TravelContext _context;
        private ILogger<TravelRepository> _logger;

        public TravelRepository(TravelContext context, ILogger<TravelRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public PGUser GetUserByName(string userName)
        {
            return _context.Users
                    .Where(x => x.UserName == userName)
                    .FirstOrDefault();
        }

        public void AddUser(PGUser user)
        {
            _context.Add(user);
        }

        public IQueryable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting All Trips from Database");

            return _context.Trips.AsQueryable();
        }

        public Trip GetTripByName(string tripName)
        {
            return _context.Trips
                .Include(t => t.Stops)
                .Where(t => t.Name == tripName)
                .FirstOrDefault();
        }

        public Trip GetTripById(int id)
        {
            return _context.Trips
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }

        public void DeleteTrip(Trip trip)
        {
            _context.Remove(trip);
        }

        public Stop GetStopById(int id)
        {
            return _context.Stops
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public void AddStop(string tripName, Stop newStop)
        {
            var trip = GetTripByName(tripName);

            if(trip != null)
            {
                trip.Stops.Add(newStop);
                _context.Stops.Add(newStop);
            }
        }
        
        public void DeleteStop(Stop stop)
        {
            { 
                _context.Remove(stop);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
