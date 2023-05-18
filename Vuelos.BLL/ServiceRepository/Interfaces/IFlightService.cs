using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Models.Entity;

namespace Vuelos.BLL.ServiceRepository.Interfaces
{
    public interface IFlightService
    {
        Task<int> Insertar(Flight modelo);
        Task<Flight> Obtener(int id);
        Task<IQueryable<Flight>> ObtenerTodos();
        Task<Flight> ObtenerPorOrigenDestino(string origen, string destino);
    }
}
