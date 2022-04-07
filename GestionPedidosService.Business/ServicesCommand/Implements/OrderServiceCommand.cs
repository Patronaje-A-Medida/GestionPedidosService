using AutoMapper;
using GestionPedidosService.Business.ServicesCommand.Interfaces;
using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Domain.Models.Orders;
using GestionPedidosService.Persistence.UnitOfWork;
using System.Linq;
using System.Threading.Tasks;

namespace GestionPedidosService.Business.ServicesCommand.Implements
{
    public class OrderServiceCommand : IOrderServiceCommand
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OrderServiceCommand(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<bool> Create(OrderCreate orderCreate)
        {
            var atelier = await _uow.atelierRepository.GetByGarmentId(orderCreate.Details.First().GarmentId);
            var lastCodeOrder = await _uow.orderRepository.GetLastCodeOrderByAtelier();

            var newOrder = _mapper.Map<Order>(orderCreate);

            var numCode = int.Parse(lastCodeOrder[4..]);
            numCode += 1;
            var newCodeOrder = "ORD-" + numCode.ToString().PadLeft(7, '0');
            newOrder.AtelierId = atelier.Id;
            newOrder.CodeOrder = newCodeOrder;

            var orderCreated = await _uow.orderRepository.Add(newOrder);

            if (orderCreated == null) return false;

            await _uow.SaveChangesAsync();
            return true;
            // ORD-0000010

        }
    }
}
