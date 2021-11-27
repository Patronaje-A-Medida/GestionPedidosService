using AutoMapper;
using GestionPedidosService.Business.Extension;
using GestionPedidosService.Business.ServicesQuery.Interfaces;
using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Domain.Models;
using GestionPedidosService.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<PagedList<OrderRead>> GetAll(OrderQuery query)
        {
            var orderEntities = await _orderDetailRepository.GetAll();
            var sortedByDate = orderEntities.OrderBy(e => e.Order.OrderDate).ToList();
            var filtered = ApplyFilters(sortedByDate, query);

            var orders = _mapper.Map<ICollection<OrderRead>>(filtered);
            return orders.ToPagedList(query.PageNumber, query.PageSize);
        }

        public async Task<OrderDetailRead> GetById(int id)
        {
            var order = await _orderDetailRepository.GetById(id);
            return _mapper.Map<OrderDetailRead>(order);
        }

        private ICollection<OrderDetail> ApplyFilters(ICollection<OrderDetail> orders, OrderQuery query)
        {
            bool hasGarmentCode = query.GarmentCode != null;
            bool hasOrderStatus = query.OrderStatus != null;

            var filtered = orders.Where(e => !hasGarmentCode || e.Garment.CodeGarment.Contains(query.GarmentCode));

            if (hasOrderStatus)
            {
                EOrderStatus orderStatus = Enum.GetValues(typeof(EOrderStatus)).Cast<EOrderStatus>()
                    .FirstOrDefault(v => v.ToDescriptionString() == query.OrderStatus);

                filtered = filtered.Where(e => !hasOrderStatus || e.Order.OrderStatus.Equals(orderStatus));
            }

            return filtered.ToList();
        }
    }
}
