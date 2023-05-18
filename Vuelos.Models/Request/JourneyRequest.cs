using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Models.Entity;

namespace Vuelos.Models.Request
{
    public class JourneyRequest
    {
        [Required]
        public string? Origin { get; set; }
        [Required]
        public string? Destination { get; set; }
    }
}
