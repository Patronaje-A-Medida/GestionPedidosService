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

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<OrderRead>), 200)]
        public async Task<ICollection<OrderRead>> GetAll()
        {
            return await _orderServiceQuery.GetAll();
        }
    }
}
