﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vuelos.Models.ApiModel
{
    public class FlightApi
    {
        public string? DepartureStation { get; set; }
        public string? ArrivalStation { get; set; }
        public string? FlightCarrier { get; set; }
        public string? FlightNumber { get; set; }
        public int Price { get; set; }
    }
}
