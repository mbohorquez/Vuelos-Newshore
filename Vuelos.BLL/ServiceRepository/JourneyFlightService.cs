using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.BLL.ServiceApi;
using Vuelos.BLL.ServiceRepository.Interfaces;
using Vuelos.BLL.Tools;
using Vuelos.DAL.Repository;
using Vuelos.DAL.ServiceApi;
using Vuelos.Models.Entity;
using Vuelos.Models.Response;

namespace Vuelos.BLL.ServiceRepository
{
    public class JourneyFlightService : IJourneyFlightService
    {
         private readonly IGenericRepository<JourneyFlight> _journeyRepository;
        private readonly IFlightApiService _flightApiService;
        private readonly IJourneyService _journeyService;
        private readonly ITransportService _transportService;
        private readonly IFlightService _flightService;
        public JourneyFlightService(IFlightApiService flightApiService,
                                    IGenericRepository<JourneyFlight> journeyRepository,
                                    IJourneyService journeyService,
                                    ITransportService transportService,
                                    IFlightService flightService)
        {
            _flightApiService = flightApiService;
            _journeyRepository = journeyRepository;
            _journeyService = journeyService;
            _transportService = transportService;
            _flightService = flightService;
        }
        public async Task<Result> ObtenerListaDeVuelosLogica(string origin, string destination)
        {
            Result result = new Result();
            RouteFinder routeFinder = new RouteFinder();
            MapRoutes mapRoutes = new MapRoutes();
            Journey journeyBD = await _journeyService.ObtenerPorOrigenDestino(origin, destination);
            if(journeyBD != null)
            {
                result.Success = true;
                result.Data = mapRoutes.RoutesMap(journeyBD);
                return result;
            }
            else
            {
                List<FlightResponse>? flights = await _flightApiService.Obtener();

                if (flights == null)
                {
                    result.Success = false;
                    result.ErrorMessage = "Error no se encontro informacion en el api";
                    return result;
                }
               
                //El ultimo parametro en la cantidad de vuelos maximos que puede llegar, se se desea se puede llegar a cargar desde el request pero la prueba no lo especifica 
                List<ListFligthResponse> routesGo = routeFinder.FindRoutes(flights, origin, destination, 10);
                List<ListFligthResponse> routesBack = routeFinder.FindRoutes(flights, destination, origin, 10);

                if (routesGo[0]?.Flights?.Count == 0 || routesBack[0]?.Flights?.Count == 0)
                {
                    result.Success = false;
                    result.ErrorMessage = "Error no se encontro rutas para ir o para volver";
                    return result;
                }

                double totalPriceGo = routesGo.Min(route => route.Price);
                double totalPriceBack = routesBack.Min(route => route.Price);

                JourneyResponse journeyResponse = new JourneyResponse
                {
                    Origin = origin,
                    Destination = destination,
                    BetterPriceGo = totalPriceGo,
                    BetterPriceBack = totalPriceBack,
                    FlightsGo = routesGo,
                    FlightsBack = routesBack
                };

                Result guardo = await GuardarRutas(journeyResponse);
                if (!guardo.Success)
                {
                    return result;
                }

                result.Success = true;
                result.Data = journeyResponse;
                return result;
            }
        }
        public async Task<Result> GuardarRutas(JourneyResponse modelo)
        {
            Result result = new Result();
            
           
             result = await GuardarTransportFlight(modelo.FlightsGo);
                if (!result.Success)
                {
                    return result;
                }
             result = await GuardarTransportFlight(modelo.FlightsBack);
                if (!result.Success)
                {
                    return result;
                }
             result = await InsertarJourney(modelo);
                if (!result.Success)
                {
                    return result;
                }
            
            return result;
        }

        public async Task<Result> GuardarTransportFlight(List<ListFligthResponse>? listFligthResponse)
        {
            Result result = new Result();

            try
            {
                foreach (var Listflight in listFligthResponse)
                {
                    foreach (var flightModel in Listflight.Flights)
                    {
                        string flightCarrier = flightModel.Transport.FlightCarrier;
                        string flightNumber = flightModel.Transport.FlightNumber;

                        Transport transport = await _transportService.ObtenerPorFlightCarrierYNumero(flightCarrier, flightNumber);

                        if (transport == null)
                        {
                            // El transporte no existe, así que se debe insertar en la base de datos
                            Transport nuevoTransporte = new Transport
                            {
                                FlightCarrier = flightCarrier,
                                FlightNumber = flightNumber
                            };
                            int idTransporte = await _transportService.Insertar(nuevoTransporte);

                            Flight flightBusqueda = await _flightService.ObtenerPorOrigenDestino(flightModel.Origin, flightModel.Destination);
                            if(flightBusqueda == null)
                            {
                                Flight nuevoVuelo = new Flight
                                {
                                    IdTransport = idTransporte,
                                    Price = flightModel.Price,
                                    Origin = flightModel.Origin,
                                    Destination = flightModel.Destination,
                                };
                                await _flightService.Insertar(nuevoVuelo);
                            }
                        }
                    }
                }
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = $"Error al Ingresar trasporte o vuelo de las rutas: {ex.Message}";
            }

            return result;
        }

        public async Task<Result> InsertarJourney(JourneyResponse journeyResponse)
        {
            Result result = new Result();

            try
            {
                Journey nuevoJourney = new Journey
                {
                    Origin = journeyResponse.Origin,
                    Destination = journeyResponse.Destination,
                    BetterPriceGo = journeyResponse.BetterPriceGo,
                    BetterPriceBack = journeyResponse.BetterPriceBack,
                };
                int idJourney = await _journeyService.Insertar(nuevoJourney);

                result = await GuardarJourneyFlight(journeyResponse.FlightsGo, idJourney, 1);
                if (!result.Success)
                {
                    return result;
                }
                result = await GuardarJourneyFlight(journeyResponse.FlightsGo, idJourney, 0);
                if (!result.Success)
                {
                    return result;
                }

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = $"Error al guardar el JourneyFlight: {ex.Message}";
            }

            return result;
        }

        public async Task<Result> GuardarJourneyFlight(List<ListFligthResponse> listFligthResponse, int idJourney, int type)
        {
            Result result = new Result();

            try
            {
                int contador = 0;
                foreach (var Listflight in listFligthResponse)
                {
                    contador++;
                    foreach (var flightModel in Listflight.Flights)
                    {
                        Flight flightBusqueda = await _flightService.ObtenerPorOrigenDestino(flightModel.Origin, flightModel.Destination);

                        JourneyFlight nuevoJourneyFlight = new JourneyFlight
                        {
                            JourneyId = idJourney,
                            Flight = flightBusqueda,
                            NRuta = contador,
                            Type = type
                        };
                        await _journeyRepository.Insertar(nuevoJourneyFlight);
                    }
                }

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = $"Error guardando el Journey: {ex.Message}";
            }

            return result;
        }
    }
}
