using GestionPedidosService.Api.Utils;
using GestionPedidosService.Business.ServicesQuery.Interfaces;
using GestionPedidosService.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestionPedidosService.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/configuration-types")]
    [Produces("application/json")]
    public class ConfigurationTypesController : ControllerBase
    {
        private readonly IDictionaryTypeServiceQuery _dictionaryTypeServiceQuery;

        public ConfigurationTypesController(IDictionaryTypeServiceQuery dictionaryTypeServiceQuery)
        {
            _dictionaryTypeServiceQuery = dictionaryTypeServiceQuery;
        }

        [HttpGet("{atelierId}")]
        [ProducesResponseType(typeof(ConfigurationTypeRead), 200)]
        [ProducesResponseType(typeof(ErrorDevDetail), 400)]
        [ProducesResponseType(typeof(ErrorDevDetail), 500)]
        public async Task<ActionResult<ConfigurationTypeRead>> GetAll(int atelierId)
        {
            var result = await _dictionaryTypeServiceQuery.GetAll(atelierId);
            return Ok(result);
        }
    }
}
