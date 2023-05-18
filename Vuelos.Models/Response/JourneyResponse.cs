using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Models.Entity;

namespace Vuelos.Models.Response
{
    public class JourneyResponse
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public double BetterPriceGo { get; set; }
        public double BetterPriceBack { get; set; }
        public List<ListFligthResponse>? FlightsGo { get; set; }
        public List<ListFligthResponse>? FlightsBack { get; set; }
    }
}
