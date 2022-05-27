using GestionPedidosService.Business.ServicesQuery.Interfaces;
using GestionPedidosService.Domain.Collections;
using GestionPedidosService.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionPedidosService.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/body-measurements")]
    [Produces("application/json")]
    public class BodyMeasurementsController : ControllerBase
    {
        private readonly IBodyMeasurementsCollectionServiceQuery _serviceQuery;

        public BodyMeasurementsController(IBodyMeasurementsCollectionServiceQuery bodyMeasurementsCollectionServiceQuery)
        {
            _serviceQuery = bodyMeasurementsCollectionServiceQuery;
        }

        [HttpGet("last-measurements/{clientId}")]
        [ProducesResponseType(typeof(BodyMeasurements), 200)]
        public async Task<BodyMeasurements> GetByClientId(int clientId)
        {
            return await _serviceQuery.GetByClientId(clientId);
        }
    }
}
