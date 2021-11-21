using AutoMapper;
using GestionPedidosService.Business.ServicesQuery.Interfaces;
using GestionPedidosService.Domain.Models;
using GestionPedidosService.Persistence.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesQuery.Implements
{
    public class OrderServiceQuery : IOrderServiceQuery
    {
        private readonly IMapper _mapper;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderServiceQuery(IMapper mapper, IOrderDetailRepository orderDetailRepository)
        {
            _mapper = mapper;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<ICollection<OrderRead>> GetAll()
        {
            var orders = await _orderDetailRepository.GetAll();
            return _mapper.Map<ICollection<OrderRead>>(orders);
        }
    }
}
