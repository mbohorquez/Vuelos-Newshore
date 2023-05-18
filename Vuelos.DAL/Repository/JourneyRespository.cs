using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.DAL.DataContext;
using Vuelos.DAL.RepositoryConfig;
using Vuelos.Models.Entity;

namespace Vuelos.DAL.Repository
{
    public class JourneyRespository : GenericRepositoryBase<Journey>
    {
        private readonly VuelosContext _dbcontext;

        public JourneyRespository(VuelosContext context)
        {
            _dbcontext = context;
        }
        public override async Task<int> Insertar(Journey modelo)
        {
            _dbcontext.Journeys.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return modelo.IdJourney;
        }

        public override async Task<Journey?> Obtener(int id)
        {
            return await _dbcontext.Journeys.FindAsync(id);
        }

        public override async Task<IQueryable<Journey>?> ObtenerTodos()
        {
            IQueryable<Journey> queryContactoSQL = _dbcontext.Journeys.Include(x => x.JourneyFlights).ThenInclude(x => x.Flight).ThenInclude(x => x.Transport);
            return queryContactoSQL;
        }
    }
}
