using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vuelos.Models.Response
{
    public class ListFligthResponse
    {
        public double Price { get; set; }
        public List<FlightResponse>? Flights { get; set; }
    }
}
