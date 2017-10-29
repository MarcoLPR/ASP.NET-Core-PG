using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_PG.Models
{
    public interface ITravelRepository
    {
        PGUser GetUserByName(string userName);
        void AddUser(PGUser user);

        IQueryable<Trip> GetAllTrips();
        Trip GetTripByName(string tripName);
        Trip GetTripById(int id);
        void AddTrip(Trip trip);
        void DeleteTrip(Trip trip);


        Stop GetStopById(int id);
        void AddStop(string tripName, Stop newStop);
        void DeleteStop(Stop stop);

        Task<bool> SaveChangesAsync();
    }
}