using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics.Contracts;
using System.Net.Http;
using Vuelos.BLL.Service;
using Vuelos.BLL.ServiceApi;
using Vuelos.BLL.ServiceRepository.Interfaces;
using Vuelos.Models.ApiModel;
using Vuelos.Models.Entity;
using Vuelos.Models.Request;
using Vuelos.Models.Response;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vuelos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VuelosController : ControllerBase
    {
        private readonly ILogger<VuelosController> _logger;
        private readonly IJourneyFlightService _journeyFlightService;
        private readonly IFlightApiService _flightApiService;
            
        public VuelosController(IJourneyFlightService journeyFlightService, ILogger<VuelosController> logger, IFlightApiService flightApiService)
        {
            _journeyFlightService = journeyFlightService;
            _flightApiService = flightApiService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerListaDeVuelos([FromBody] JourneyRequest journeyRequest)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Error al validar el modelo");
                return BadRequest(ModelState);
            }
            Result? listJourneyResponse = await _journeyFlightService.ObtenerListaDeVuelosLogica(journeyRequest.Origin, journeyRequest.Destination);
            if (!listJourneyResponse.Success)
            {
                _logger.LogError(listJourneyResponse.ErrorMessage);
                return NotFound(listJourneyResponse.ErrorMessage);
            }
            return Ok(listJourneyResponse.Data);
        }


        [HttpGet("consumiendoApi")]
        public async Task<IActionResult> ObtenerApi()
        {
            List<FlightResponse>? flights = await _flightApiService.Obtener();

            if (flights?.Count !> 0 || flights == null)
            {
                _logger.LogInformation("Error en la Api de _flightApiService");
                return NotFound();
            }
            return Ok(flights);
        }
    }
}
