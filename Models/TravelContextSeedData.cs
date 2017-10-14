using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_PG.Models
{
    public class TravelContextSeedData
    {
        private TravelContext _context;
        private UserManager<PGUser> _userManager;

        public TravelContextSeedData(TravelContext context, UserManager<PGUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedData()
        {
            if (await _userManager.FindByEmailAsync("marcolpr98@gmail.com") == null)
            {
                var user = new PGUser()
                {
                    UserName = "MarcoLPR",
                    Email = "marcolpr98@gmail.com",
                };

                await _userManager.CreateAsync(user, "Pepe000!");
            }

            if (!_context.Trips.Any())
            {
                var usTrip = new Trip()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "US Trip",
                    UserName = "MarcoLPR",
                    Stops = new List<Stop>()
                    {
                        new Stop() {  Name = "Atlanta, GA", Arrival = new DateTime(2014, 6, 4),  Order = 0 },
                        new Stop() {  Name = "New York, NY", Arrival = new DateTime(2014, 6, 9),  Order = 1 },
                        new Stop() {  Name = "Boston, MA", Arrival = new DateTime(2014, 7, 1),  Order = 2 },
                        new Stop() {  Name = "Chicago, IL", Arrival = new DateTime(2014, 7, 10),  Order = 3 },
                        new Stop() {  Name = "Seattle, WA", Arrival = new DateTime(2014, 8, 13),  Order = 4 },
                        new Stop() {  Name = "Atlanta, GA", Arrival = new DateTime(2014, 8, 23),  Order = 5 },
                    }
                };

                _context.Trips.Add(usTrip);

                _context.Stops.AddRange(usTrip.Stops);

                var worldTrip = new Trip()
                {
                    DateCreated = DateTime.UtcNow,
                    Name = "World Trip",
                    UserName = "MarcoLPR",
                    Stops = new List<Stop>()
                    {
                        new Stop() { Order = 0, Name = "Atlanta, Georgia", Arrival = DateTime.Parse("Jun 3, 2014") },
                        new Stop() { Order = 1, Name = "Paris, France", Arrival = DateTime.Parse("Jun 4, 2014") },
                        new Stop() { Order = 2, Name = "Brussels, Belgium", Arrival = DateTime.Parse("Jun 25, 2014") },
                        new Stop() { Order = 3, Name = "Bruges, Belgium", Arrival = DateTime.Parse("Jun 28, 2014") },
                        new Stop() { Order = 4, Name = "Paris, France", Arrival = DateTime.Parse("Jun 30, 2014") },
                        new Stop() { Order = 5,  Name = "London, UK", Arrival = DateTime.Parse("Jul 8, 2014") },
                        new Stop() { Order = 6,  Name = "Bristol, UK", Arrival = DateTime.Parse("Jul 24, 2014") },
                        new Stop() { Order = 7,  Name = "Stretton Sugwas, UK", Arrival = DateTime.Parse("Jul 29, 2014") },
                        new Stop() { Order = 8,  Name = "Gloucestershire, UK", Arrival = DateTime.Parse("Jul 30, 2014") },
                        new Stop() { Order = 9,  Name = "Nottingham, UK", Arrival = DateTime.Parse("Jul 31, 2014") },
                        new Stop() { Order = 10, Name = "London, UK", Arrival = DateTime.Parse("Aug 1, 2014") },
                        new Stop() { Order = 11, Name = "Edinburgh, UK", Arrival = DateTime.Parse("Aug 5, 2014") },
                        new Stop() { Order = 12, Name = "Glasgow, UK", Arrival = DateTime.Parse("Aug 6, 2014") },
                        new Stop() { Order = 13, Name = "Aberdeen, UK", Arrival = DateTime.Parse("Aug 7, 2014") },
                        new Stop() { Order = 14, Name = "Edinburgh, UK", Arrival = DateTime.Parse("Aug 8, 2014") },
                        new Stop() { Order = 15, Name = "London, UK", Arrival = DateTime.Parse("Aug 10, 2014") },
                        new Stop() { Order = 16, Name = "Amsterdam, Netherlands", Arrival = DateTime.Parse("Aug 14, 2014") },
                        new Stop() { Order = 17, Name = "Strasbourg, France", Arrival = DateTime.Parse("Aug 17, 2014") },
                        new Stop() { Order = 18, Name = "Lausanne, Switzerland", Arrival = DateTime.Parse("Aug 19, 2014") },
                        new Stop() { Order = 19, Name = "Zermatt, Switzerland", Arrival = DateTime.Parse("Aug 27, 2014") },
                        new Stop() { Order = 20, Name = "Lausanne, Switzerland", Arrival = DateTime.Parse("Aug 29, 2014") },
                        new Stop() { Order = 21, Name = "Dublin, Ireland", Arrival = DateTime.Parse("Sep 2, 2014") },
                        new Stop() { Order = 22, Name = "Belfast, Northern Ireland", Arrival = DateTime.Parse("Sep 7, 2014") },
                        new Stop() { Order = 23, Name = "Dublin, Ireland", Arrival = DateTime.Parse("Sep 9, 2014") },
                        new Stop() { Order = 24, Name = "Zurich, Switzerland", Arrival = DateTime.Parse("Sep 16, 2014") },
                        new Stop() { Order = 25, Name = "Munich, Germany", Arrival = DateTime.Parse("Sep 19, 2014") },
                        new Stop() { Order = 26, Name = "Prague, Czech Republic", Arrival = DateTime.Parse("Sep 21, 2014") },
                        new Stop() { Order = 27, Name = "Dresden, Germany", Arrival = DateTime.Parse("Oct 1, 2014") },
                        new Stop() { Order = 28, Name = "Prague, Czech Republic", Arrival = DateTime.Parse("Oct 4, 2014") },
                        new Stop() { Order = 29, Name = "Dubrovnik, Croatia", Arrival = DateTime.Parse("Oct 10, 2014") },
                        new Stop() { Order = 30, Name = "Sofia, Bulgaria", Arrival = DateTime.Parse("Oct 16, 2014") },
                        new Stop() { Order = 31, Name = "Brosov, Romania", Arrival = DateTime.Parse("Oct 20, 2014") },
                        new Stop() { Order = 32, Name = "Istanbul, Turkey", Arrival = DateTime.Parse("Nov 1, 2014") },
                        new Stop() { Order = 33, Name = "Zagreb, Croatia", Arrival = DateTime.Parse("Nov 11, 2014") },
                        new Stop() { Order = 34, Name = "Istanbul, Turkey", Arrival = DateTime.Parse("Nov 15, 2014") },
                        new Stop() { Order = 35, Name = "Brussels, Belgium", Arrival = DateTime.Parse("Nov 25, 2014") },
                        new Stop() { Order = 36, Name = "Cologne, Germany", Arrival = DateTime.Parse("Nov 30, 2014") },
                        new Stop() { Order = 37, Name = "Vienna, Austria", Arrival = DateTime.Parse("Dec 4, 2014") },
                        new Stop() { Order = 38, Name = "Budapest, Hungary", Arrival = DateTime.Parse("Dec 28,2014") },
                        new Stop() { Order = 39, Name = "Athens, Greece", Arrival = DateTime.Parse("Jan 2, 2015") },
                        new Stop() { Order = 40, Name = "Pretoria, South Africa", Arrival = DateTime.Parse("Jan 19, 2015") },
                        new Stop() { Order = 41, Name = "Florence, Italy", Arrival = DateTime.Parse("Feb 1, 2015") },
                        new Stop() { Order = 42, Name = "Venice, Italy", Arrival = DateTime.Parse("Feb 9, 2015") },
                        new Stop() { Order = 43, Name = "Florence, Italy", Arrival = DateTime.Parse("Feb 13, 2015") },
                        new Stop() { Order = 44, Name = "Rome, Italy", Arrival = DateTime.Parse("Feb 17, 2015") },
                        new Stop() { Order = 45, Name = "New Delhi, India", Arrival = DateTime.Parse("Mar 4, 2015") },
                        new Stop() { Order = 46, Name = "Kathmandu, Nepal", Arrival = DateTime.Parse("Mar 10, 2015") },
                        new Stop() { Order = 47, Name = "New Delhi, India", Arrival = DateTime.Parse("Mar 11, 2015") },
                        new Stop() { Order = 48, Name = "Macau", Arrival = DateTime.Parse("Mar 21, 2015") },
                        new Stop() { Order = 49, Name = "Hong Kong", Arrival = DateTime.Parse("Mar 24, 2015") },
                        new Stop() { Order = 50, Name = "Beijing, China", Arrival = DateTime.Parse("Apr 19, 2015") },
                        new Stop() { Order = 51, Name = "Hong Kong", Arrival = DateTime.Parse("Apr 24, 2015") },
                        new Stop() { Order = 52, Name = "Singapore", Arrival = DateTime.Parse("Apr 30, 2015") },
                        new Stop() { Order = 53, Name = "Kuala Lumpor, Malaysia", Arrival = DateTime.Parse("May 7, 2015") },
                        new Stop() { Order = 54, Name = "Bangkok, Thailand", Arrival = DateTime.Parse("May 24, 2015") },
                        new Stop() { Order = 55, Name = "Atlanta, Georgia", Arrival = DateTime.Parse("Jun 17, 2015") },
                    }
                };

                _context.Trips.Add(worldTrip);

                _context.Stops.AddRange(worldTrip.Stops);

                await _context.SaveChangesAsync();
            }
        }
    }
}
