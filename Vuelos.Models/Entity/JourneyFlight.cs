using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vuelos.Models.Entity
{
    public class JourneyFlight
    {
        [Key]
        [Column(Order = 0)]
        public int JourneyId { get; set; }

        [Key]
        [Column(Order = 3)]
        public int FlightId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int NRuta { get; set; }
        [Key]
        [Column(Order = 1)]
        public int Type { get; set; }

        [ForeignKey("JourneyId")]
        public virtual Journey? Journey { get; set; }

        [ForeignKey("FlightId")]
        public virtual Flight? Flight { get; set; }
    }
}
