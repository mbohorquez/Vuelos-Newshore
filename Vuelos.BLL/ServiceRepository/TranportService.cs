using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.BLL.ServiceRepository.Interfaces;
using Vuelos.DAL.Repository;
using Vuelos.Models.Entity;

namespace Vuelos.BLL.Service
{
    public class TranportService : ITransportService
    {
        private readonly IGenericRepository<Transport> _transportRepo;

        public TranportService(IGenericRepository<Transport> transportRepo)
        {
            _transportRepo = transportRepo;
        }

        public async Task<int> Insertar(Transport modelo)
        {
            return await _transportRepo.Insertar(modelo);
        }

        public async Task<Transport> Obtener(int id)
        {
            return await _transportRepo.Obtener(id);
        }

        public async Task<Transport> ObtenerPorFlightCarrierYNumero(string carrier, string numero)
        {
            Transport? transport = new Transport();
            IQueryable<Transport> queryTransportSQL = await _transportRepo.ObtenerTodos();
            if (queryTransportSQL.Any())
            {
                transport = queryTransportSQL.Where(c => c.FlightCarrier == carrier && c.FlightNumber == numero).FirstOrDefault();
                return transport;
            }

            return null;
        }

        public async Task<IQueryable<Transport>> ObtenerTodos()
        {
            return await _transportRepo.ObtenerTodos();
        }
    }
}
