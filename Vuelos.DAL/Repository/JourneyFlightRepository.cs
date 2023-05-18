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
    public class JourneyFlightRepository : GenericRepositoryBase<JourneyFlight>
    {
        private readonly VuelosContext _dbcontext;

        public JourneyFlightRepository(VuelosContext context)
        {
            _dbcontext = context;
        }
        public override async Task<int> Insertar(JourneyFlight modelo)
        {
            _dbcontext.JourneyFlights.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return modelo.JourneyId;
        }

        public override async Task<JourneyFlight?> Obtener(int id)
        {
            return await _dbcontext.JourneyFlights.FindAsync(id);
        }

        public override async Task<IQueryable<JourneyFlight>?> ObtenerTodos()
        {
            IQueryable<JourneyFlight> queryContactoSQL = _dbcontext.JourneyFlights;
            return queryContactoSQL;
        }
    }
}
