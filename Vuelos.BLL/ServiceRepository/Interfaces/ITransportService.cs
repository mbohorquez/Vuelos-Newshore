using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Models.Entity;

namespace Vuelos.BLL.ServiceRepository.Interfaces
{
    public interface ITransportService
    {
        Task<int> Insertar(Transport modelo);
        Task<Transport> Obtener(int id);
        Task<IQueryable<Transport>> ObtenerTodos();
        Task<Transport> ObtenerPorFlightCarrierYNumero(string carrier, string numero);
    }

}
