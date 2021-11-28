using GestionPedidosService.Business.ServicesQuery.Interfaces;
using GestionPedidosService.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpPost]
        [ProducesResponseType(typeof(ICollection<OrderRead>), 200)]
        public async Task<PagedList<OrderRead>> GetAll([FromBody] OrderQuery query)
        {
            return await _orderServiceQuery.GetAll(query);
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
    }
}
