using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Models.Entity;

namespace Vuelos.Models.Response
{
    public class FlightResponse
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public double Price { get; set; }
        public TransportResponse? Transport { get; set; }
    }
}
