﻿namespace ASP.NET_Core_PG.Services
{
    public class GeoCoordsResults
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}