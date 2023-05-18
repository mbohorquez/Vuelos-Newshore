using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vuelos.Models.Entity
{
    public class Flight
    {
        [Key]
        public int IdFlight { get; set; }
        [Required]
        public int IdTransport { get; set; }
        [Required]
        public string? Origin { get; set; }
        [Required]
        public string? Destination { get; set; }
        [Required]
        public double Price { get; set; }

        [ForeignKey("IdTransport")]
        public virtual Transport? Transport { get; set; }
    }
}
