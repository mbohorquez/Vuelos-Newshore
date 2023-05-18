using Azure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Models.ApiModel;
using Vuelos.Models.Entity;

namespace Vuelos.DAL.ServiceApi
{
    public class FlightApiRepository : IFlightApiRepository
    {
        private readonly HttpClient _httpClient;

        public FlightApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://recruiting-api.newshore.es/api/flights");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        public async Task<HttpResponseMessage> Obtener()
        {
            var response = await _httpClient.GetAsync("/api/flights/2");
            return response;
        }
    }
}
