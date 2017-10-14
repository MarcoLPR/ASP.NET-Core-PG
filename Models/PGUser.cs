using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_PG.Models
{
    public class PGUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }
}
