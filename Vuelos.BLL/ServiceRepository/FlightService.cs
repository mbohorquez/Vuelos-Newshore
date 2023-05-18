using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.BLL.ServiceRepository.Interfaces;
using Vuelos.DAL.Repository;
using Vuelos.Models.Entity;

namespace Vuelos.BLL.ServiceRepository
{
    public class FlightService : IFlightService
    {
        private readonly IGenericRepository<Flight> _flightRepo;

        public FlightService(IGenericRepository<Flight> flightRepo)
        {
            _flightRepo = flightRepo;
        }

        public async Task<int> Insertar(Flight modelo)
        {
            return await _flightRepo.Insertar(modelo);
        }

        public async Task<Flight> Obtener(int id)
        {
            return await _flightRepo.Obtener(id);
        }

        public async Task<IQueryable<Flight>> ObtenerTodos()
        {
            return await _flightRepo.ObtenerTodos();
        }

        public async Task<Flight> ObtenerPorOrigenDestino(string origen, string destino)
        {
            Flight? flight = new Flight();
            IQueryable<Flight> queryJourneySQL = await _flightRepo.ObtenerTodos();
            if (queryJourneySQL.Any())
            {
                flight = queryJourneySQL.Where(c => c.Origin == origen && c.Destination == destino).FirstOrDefault();
                return flight;
            }

            return null;
        }
    }
}
