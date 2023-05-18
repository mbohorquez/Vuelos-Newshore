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
    public class FlightRepository : GenericRepositoryBase<Flight>
    {
        private readonly VuelosContext _dbcontext;

        public FlightRepository(VuelosContext context)
        {
            _dbcontext = context;
        }
        public override async Task<int> Insertar(Flight modelo)
        {
            _dbcontext.Flights.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return modelo.IdFlight;
        }

        public override async Task<Flight?> Obtener(int id)
        {
            return await _dbcontext.Flights.FindAsync(id);
        }

        public override async Task<IQueryable<Flight>?> ObtenerTodos()
        {
            IQueryable<Flight> queryContactoSQL = _dbcontext.Flights;
            return queryContactoSQL;
        }
    }
}
