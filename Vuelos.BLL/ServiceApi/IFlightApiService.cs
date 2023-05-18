using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Models.ApiModel;
using Vuelos.Models.Entity;
using Vuelos.Models.Response;

namespace Vuelos.BLL.ServiceApi
{
    public interface IFlightApiService
    {
        Task<List<FlightResponse>?> Obtener();

    }
}
