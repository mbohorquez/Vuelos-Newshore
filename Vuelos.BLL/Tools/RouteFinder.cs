using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Models.Entity;
using Vuelos.Models.Response;

namespace Vuelos.BLL.Tools
{
    public class RouteFinder
    {
        private List<ListFligthResponse>? routes;
        private int maxFlightsPerRoute; // Número máximo de vuelos por ruta
        private string? _origin;

        public List<ListFligthResponse> FindRoutes(List<FlightResponse> flights, string origin, string destination, int maxFlights)
        {
            routes = new List<ListFligthResponse>();
            List<FlightResponse> currentRoute = new List<FlightResponse>();
            maxFlightsPerRoute = maxFlights;
            _origin = origin;
            DestinationFinder(flights, origin, destination, currentRoute, 0);
            return routes;
        }

        private void DestinationFinder(List<FlightResponse> flights, string currentAirport, string destination, List<FlightResponse> currentRoute, int flightCount)
        {
            if (currentRoute.Any(f => f.Destination == _origin)) // Se mantiene  la validacion contra el originen inicial
            {
                return; // Evitar rutas circulares
            }
            if (currentAirport == destination)
            {
                double totalPrice = currentRoute.Sum(f => f.Price);
                ListFligthResponse listFligthResponse = new ListFligthResponse
                {
                    Price = totalPrice,
                    Flights = new List<FlightResponse>(currentRoute)
                };

                // Agregar una copia de la lista currentRoute a routes
                routes?.Add(listFligthResponse);
            }
            else if (flightCount < maxFlightsPerRoute) // Verificar el numero de vuelos actual
            {
                foreach (var flight in flights)
                {
                    if (flight.Origin == currentAirport && !currentRoute.Contains(flight))
                    {
                        // Se realiza mejora buscando si existe un destino repedito como origen ya en el current
                        bool isCircular = currentRoute.Any(route => route.Origin == flight.Destination);
                        if (!isCircular)
                        {
                            currentRoute.Add(flight);
                            DestinationFinder(flights, flight.Destination, destination, currentRoute, flightCount + 1); // Incrementar el contador de vuelos
                            currentRoute.Remove(flight);
                        }
                    }
                }
            }
        }
    }
}
