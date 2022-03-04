using GestionPedidosService.Api.Extensions;
using GestionPedidosService.Api.Utils;
using GestionPedidosService.Business.Handlers;
using GestionPedidosService.Business.ServicesQuery.Interfaces;
using GestionPedidosService.Domain.Models;
using Microsoft.AspNetCore.Diagnostics;
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
    [Route("api/v1/orders")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServiceQuery _orderServiceQuery;

        public OrderController(IOrderServiceQuery orderServiceQuery)
        {
            _orderServiceQuery = orderServiceQuery;
        }

        [HttpPost("by-query")]
        [ProducesResponseType(typeof(ICollection<OrderRead>), 200)]
        [ProducesResponseType(typeof(ErrorDevDetail), 400)]
        [ProducesResponseType(typeof(ErrorDevDetail), 500)]
        public async Task<ActionResult<PagedList<OrderRead>>> GetAllByQuery([FromBody] OrderQuery query)
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

                var result = await _orderServiceQuery.GetAllByQuery(query);
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

        [HttpGet("{orderDetailId}")]
        [ProducesResponseType(typeof(OrderDetailRead), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult<OrderDetailRead>> GetById(int orderDetailId)
        {
            var result = await _orderServiceQuery.GetById(orderDetailId);

            if (result == null)
            {
                var errorMessage = $"There is no order detail with id '{orderDetailId}'";
                return NotFound(new { StatusCode = 404, ErroMessage = errorMessage });
            }

            return Ok(result);
        }

        [HttpGet("details")]
        [ProducesResponseType(typeof(OrderDetailRead), 200)]
        [ProducesResponseType(typeof(ErrorDevDetail), 404)]
        [ProducesResponseType(typeof(ErrorDevDetail), 400)]
        [ProducesResponseType(typeof(ErrorDevDetail), 500)]
        public async Task<ActionResult<OrderDetailRead>> GetByCodeOrder_CodeGarment([FromQuery] string codeOrder, [FromQuery] string codeGarment)
        {
            try
            {
                var result = await _orderServiceQuery.GetByCodeOrder_CodeGarment(codeOrder, codeGarment);
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
