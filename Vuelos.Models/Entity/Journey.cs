using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Models.Response;

namespace Vuelos.Models.Entity
{
    public class Journey
    {
        [Key]
        public int IdJourney { get; set; }
        [Required]
        public string? Origin { get; set; }
        [Required]
        public string? Destination { get; set; }
        public double BetterPriceGo { get; set; }
        public double BetterPriceBack { get; set; }

        public ICollection<JourneyFlight> JourneyFlights { get; set; }

        public Journey()
        {
            JourneyFlights = new List<JourneyFlight>();
        }

    }
}
