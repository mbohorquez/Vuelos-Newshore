using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Models.ApiModel;
using Vuelos.Models.Entity;

namespace Vuelos.DAL.ServiceApi
{
    public interface IFlightApiRepository
    {
        Task<HttpResponseMessage> Obtener();

    }
}
