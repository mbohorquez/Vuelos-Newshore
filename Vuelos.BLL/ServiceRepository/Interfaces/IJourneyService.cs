using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Models.Entity;

namespace Vuelos.BLL.ServiceRepository.Interfaces
{
    public interface IJourneyService
    {
        Task<int> Insertar(Journey modelo);
        Task<Journey> Obtener(int id);
        Task<IQueryable<Journey>> ObtenerTodos();
        Task<Journey> ObtenerPorOrigenDestino(string origen, string destino);
    }
}
