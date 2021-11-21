using GestionPedidosService.Business.ServicesQuery.Interfaces;
using GestionPedidosService.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionPedidosService.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/pattern-garments")]
    [Produces("application/json")]
    public class PatternGarmentController : ControllerBase
    {
        private readonly IPatternGarmentServiceQuery _patternGarmentServiceQuery;

        public PatternGarmentController(IPatternGarmentServiceQuery patternGarmentServiceQuery)
        {
            _patternGarmentServiceQuery = patternGarmentServiceQuery;
        }

        [HttpGet("{garmentId}")]
        [ProducesResponseType(typeof(ICollection<PatternGarmentRead>), 200)]
        public async Task<ICollection<PatternGarmentRead>> GetAllByGarmentId(int garmentId)
        {
            return await _patternGarmentServiceQuery.GetAllByGarmentId(garmentId);
        }
    }
}
