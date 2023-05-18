using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vuelos.Models.Entity;

public class Transport
{
    [Key]
    public int IdTransport { get; set; }
    [Required]
    public string? FlightCarrier { get; set; }
    [Required]
    public string? FlightNumber { get; set; }
}
