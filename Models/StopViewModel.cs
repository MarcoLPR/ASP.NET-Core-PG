using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_PG.Models
{
    public class StopViewModel
    {

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Name { get; set; }
        
        [Required]
        public int Order { get; set; }

        [Required]
        public DateTime Arrival { get; set; }

        [Required]
        public double Long { get; set; }

        [Required]
        public double Lat { get; set; }
    }
}
