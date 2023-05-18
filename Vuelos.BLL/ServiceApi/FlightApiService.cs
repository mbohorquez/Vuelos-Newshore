using Azure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.DAL.ServiceApi;
using Vuelos.Models.ApiModel;
using Vuelos.Models.Entity;
using Vuelos.Models.Response;

namespace Vuelos.BLL.ServiceApi
{
    public class FlightApiService : IFlightApiService
    {
        private readonly IFlightApiRepository _repository;

        public FlightApiService(IFlightApiRepository flightApiRepository)
        {
            _repository = flightApiRepository;
        }
        public async Task<List<FlightResponse>?> Obtener()
        {
            List<FlightResponse> flightList = new List<FlightResponse>();
            var response = await _repository.Obtener();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                List<FlightApi>? flightApiList = JsonConvert.DeserializeObject<List<FlightApi>>(content);

                if (flightApiList?.Count > 0)
                {

                    flightList = flightApiList.Select(flightApi => new FlightResponse
                    {
                        Origin = flightApi.DepartureStation,
                        Destination = flightApi.ArrivalStation,
                        Price = flightApi.Price,
                        Transport = new TransportResponse
                        {
                            FlightCarrier = flightApi.FlightCarrier,
                            FlightNumber = flightApi.FlightNumber
                        }
                    }).ToList();
                }

                return flightList;
            }
            else
            {
                return null;
            }

        }
    }
}
