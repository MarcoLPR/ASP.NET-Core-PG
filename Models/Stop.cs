using System;

namespace ASP.NET_Core_PG.Models
{
    public class Stop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public DateTime Arrival { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}