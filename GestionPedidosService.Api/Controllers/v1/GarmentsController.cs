using GestionPedidosService.Api.Utils;
using GestionPedidosService.Business.Handlers;
using GestionPedidosService.Business.ServicesQuery.Interfaces;
using GestionPedidosService.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static GestionPedidosService.Domain.Utils.ErrorsUtil;

namespace GestionPedidosService.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class GarmentsController : ControllerBase
    {
        private readonly IGarmentServiceQuery _garmentServiceQuery;

        public GarmentsController(IGarmentServiceQuery garmentServiceQuery)
        {
            _garmentServiceQuery = garmentServiceQuery;
        }

        [HttpPost("by-query")]
        [ProducesResponseType(typeof(IEnumerable<GarmentMin>), 200)]
        [ProducesResponseType(typeof(ErrorDevDetail), 400)]
        [ProducesResponseType(typeof(ErrorDevDetail), 500)]
        public async Task<ActionResult<IEnumerable<GarmentMin>>> GetAllByQuery([FromBody] GarmentQuery garmentQuery)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string err = string.Join(
                        "; ",
                        ModelState.Values
                            .SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage));

                    return BadRequest(new ErrorDetail
                    {
                        statusCode = (int)HttpStatusCode.BadRequest,
                        errorCode = ErrorsCode.INVALID_MODEL_ERROR,
                        message = err
                    });
                }
                var result = await _garmentServiceQuery.GetAllByQuery(garmentQuery);
                return Ok(result);
            }
            catch(ServiceException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
