using GestionPedidosService.Business.ServicesCommand.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestionPedidosService.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/data-files")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Produces("application/json")]
    public class DataController : ControllerBase
    {
        private readonly IGarmentServiceCommand _garmentServiceCommand;

        public DataController(IGarmentServiceCommand garmentServiceCommand)
        {
            _garmentServiceCommand = garmentServiceCommand;
        }


        [HttpPost("uploads-data")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<bool>> UploadTemporal(IFormFile file_frontal, IFormFile file_lateral)
        {
            var result = await _garmentServiceCommand.UploadTemporal(file_frontal, file_lateral);
            return Ok(result);
        }

    }
}
