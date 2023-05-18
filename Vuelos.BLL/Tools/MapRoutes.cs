using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Models.Entity;
using Vuelos.Models.Response;

namespace Vuelos.BLL.Tools
{
    public class MapRoutes
    {
        private JourneyResponse? routes;


        public JourneyResponse RoutesMap(Journey journeyModel)
        {
            JourneyResponse journey = new JourneyResponse
            {
                Origin = journeyModel.Origin,
                Destination = journeyModel.Destination,
                BetterPriceGo = journeyModel.BetterPriceGo,
                BetterPriceBack = journeyModel.BetterPriceBack,
                FlightsGo = RouteGoOrBack(journeyModel.JourneyFlights.Where(x => x.Type == 0).OrderBy(x => x.NRuta).ToList()),
                FlightsBack = RouteGoOrBack(journeyModel.JourneyFlights.Where(x => x.Type == 1).OrderBy(x => x.NRuta).ToList())
            };
            return journey;
        }

        public List<ListFligthResponse> RouteGoOrBack(List<JourneyFlight> JourneyFlightModel)
        {
            List<ListFligthResponse> listaVuelosPrecios = new List<ListFligthResponse>();

            for (int i = 1; i <= JourneyFlightModel.Last().NRuta; i++)
            {
                List<JourneyFlight> JourneyFlightNRuta = JourneyFlightModel.Where(x => x.NRuta == i).ToList();
                List<FlightResponse> listaVuelos = new List<FlightResponse>();
                for (int e = 0; e <= (JourneyFlightNRuta.Count - 1); e++)
                {
                    FlightResponse flightResponse = new FlightResponse
                    {
                        Origin = JourneyFlightNRuta[e].Flight.Origin,
                        Destination = JourneyFlightNRuta[e].Flight.Destination,
                        Price = JourneyFlightNRuta[e].Flight.Price,
                        Transport = new TransportResponse
                        {
                            FlightCarrier = JourneyFlightNRuta[e].Flight.Transport.FlightCarrier,
                            FlightNumber = JourneyFlightNRuta[e].Flight.Transport.FlightNumber
                        }
                    };
                    listaVuelos.Add(flightResponse);
                }
                ListFligthResponse listFligthResponse = new ListFligthResponse
                {
                    Price = listaVuelos.Sum(f => f.Price),
                    Flights = listaVuelos
                };
                listaVuelosPrecios.Add(listFligthResponse);
            }
            return listaVuelosPrecios;
        }


    }
}
