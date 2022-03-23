using GestionPedidosService.Api.Utils;
using GestionPedidosService.Business.Handlers;
using GestionPedidosService.Business.ServicesCommand.Interfaces;
using GestionPedidosService.Business.ServicesQuery.Interfaces;
using GestionPedidosService.Domain.Models.Garments;
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
        private readonly IGarmentServiceCommand _garmentServiceCommand;

        public GarmentsController(IGarmentServiceQuery garmentServiceQuery, IGarmentServiceCommand garmentServiceCommand)
        {
            _garmentServiceQuery = garmentServiceQuery;
            _garmentServiceCommand = garmentServiceCommand;
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

        [HttpPost("save")]
        [DisableRequestSizeLimit]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(ErrorDevDetail), 400)]
        [ProducesResponseType(typeof(ErrorDevDetail), 500)]
        public async Task<ActionResult<bool>> Save(GarmentWrite garmentWrite)
        {
            try
            {
                var result = await _garmentServiceCommand.Save(garmentWrite);
                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ErrorDetail
                    {
                        statusCode = (int)HttpStatusCode.BadRequest,
                        errorCode = ErrorsCode.ADD_GARMENT_FAILED,
                        message = ErrorMessages.ADD_GARMENT_FAILED
                    });
                }
            }
            catch (ServiceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("upload-images-base64")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadGarmentImages ([FromBody] GarmentImageString garmentImage)
        {
            var result = await _garmentServiceCommand.UploadGarmentImages(garmentImage);
            return Ok(result);
        }

        [HttpPost("upload-images-file")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadGarmentImages([FromForm] GarmentImageFile garmentImage)
        {
            var result = await _garmentServiceCommand.UploadGarmentImages(garmentImage);
            return Ok(result);
        }

        [HttpGet("details")]
        [ProducesResponseType(typeof(GarmentRead), 200)]
        [ProducesResponseType(typeof(ErrorDevDetail), 404)]
        [ProducesResponseType(typeof(ErrorDevDetail), 400)]
        [ProducesResponseType(typeof(ErrorDevDetail), 500)]
        public async Task<ActionResult<GarmentRead>> GetByCodeGarment_AtelierId([FromQuery] string codeGarment, int atelierId)
        {
            try
            {
                var result = await _garmentServiceQuery.GetByCodeGarment_AtelierId(codeGarment, atelierId);

                if (result == null)
                {
                    return NotFound(new ErrorDetail
                    {
                        errorCode = ErrorsCode.NOT_FOUND_ORDER,
                        message = ErrorMessages.NOT_FOUND_ORDER,
                        statusCode = (int)HttpStatusCode.NotFound,
                    });
                }

                return Ok(result);
            }
            catch (ServiceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
