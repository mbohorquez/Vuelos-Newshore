using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vuelos.Models.Entity;
using Vuelos.Models.Response;

namespace Vuelos.BLL.ServiceRepository.Interfaces
{
    public interface IJourneyFlightService
    {
        Task<Result> ObtenerListaDeVuelosLogica(string origin, string destination);
        Task<Result> GuardarRutas(JourneyResponse modelo);
        Task<Result> GuardarTransportFlight(List<ListFligthResponse> modelo);
        Task<Result> InsertarJourney(JourneyResponse modelo);
        Task<Result> GuardarJourneyFlight(List<ListFligthResponse> modelo, int idJourney, int type);
    }
}
