using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.BLL.ServiceRepository.Interfaces;
using Vuelos.DAL.Repository;
using Vuelos.Models.Entity;

namespace Vuelos.BLL.ServiceRepository
{
    public class JourneyService : IJourneyService
    {
        private readonly IGenericRepository<Journey> _journeyRepo;

        public JourneyService(IGenericRepository<Journey> journeyRepo)
        {
            _journeyRepo = journeyRepo;
        }

        public async Task<int> Insertar(Journey modelo)
        {
            return await _journeyRepo.Insertar(modelo);
        }

        public async Task<Journey> Obtener(int id)
        {
            return await _journeyRepo.Obtener(id);
        }

        public async Task<Journey> ObtenerPorOrigenDestino(string origen, string destino)
        {
            Journey? journey = new Journey();
            IQueryable<Journey> queryJourneySQL = await _journeyRepo.ObtenerTodos();
            if(queryJourneySQL.Any()) {
                journey = queryJourneySQL.Where(c => c.Origin == origen && c.Destination == destino).FirstOrDefault();
                return journey;
            }
            
            return null;
        }

        public async Task<IQueryable<Journey>> ObtenerTodos()
        {
            return await _journeyRepo.ObtenerTodos();
        }
    }
}
