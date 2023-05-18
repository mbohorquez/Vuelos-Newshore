using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.DAL.DataContext;
using Vuelos.DAL.RepositoryConfig;
using Vuelos.Models.Entity;

namespace Vuelos.DAL.Repository
{
    public class TransportRepository : GenericRepositoryBase<Transport>
    {
        private readonly VuelosContext _dbcontext;

        public TransportRepository(VuelosContext context)
        {
            _dbcontext = context;
        }
        public override async Task<int> Insertar(Transport modelo)
        {
            _dbcontext.Transports.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return modelo.IdTransport;
        }

        public override async Task<Transport?> Obtener(int id)
        {
            return await _dbcontext.Transports.FindAsync(id);
        }

        public override async Task<IQueryable<Transport>?> ObtenerTodos()
        {
            IQueryable<Transport> queryContactoSQL = _dbcontext.Transports;
            return queryContactoSQL;
        }
    }
}
