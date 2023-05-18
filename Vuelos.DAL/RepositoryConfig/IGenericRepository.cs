using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vuelos.DAL.Repository
{
    public interface IGenericRepository<TEntityModel> where TEntityModel : class
    {
        Task<int> Insertar(TEntityModel modelo);
        Task<bool> Actualizar(TEntityModel modelo);
        Task<bool> Eliminar(int id);
        Task<TEntityModel> Obtener(int id);
        Task<IQueryable<TEntityModel>> ObtenerTodos();
    }
}
