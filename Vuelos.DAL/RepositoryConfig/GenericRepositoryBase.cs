using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.DAL.Repository;

namespace Vuelos.DAL.RepositoryConfig
{
    public abstract class GenericRepositoryBase<TEntityModel> : IGenericRepository<TEntityModel> where TEntityModel : class
    {
        public virtual Task<int> Insertar(TEntityModel modelo)
        {
            return Task.FromResult(0);
        }

        public virtual Task<bool> Actualizar(TEntityModel modelo)
        {
            return Task.FromResult(true);
        }

        public virtual Task<bool> Eliminar(int id)
        {
            return Task.FromResult(true);
        }

        public virtual Task<TEntityModel?> Obtener(int id)
        {
            return Task.FromResult(default(TEntityModel));
        }

        public virtual Task<IQueryable<TEntityModel>> ObtenerTodos()
        {
            return Task.FromResult(Enumerable.Empty<TEntityModel>().AsQueryable());
        }
    }
}
