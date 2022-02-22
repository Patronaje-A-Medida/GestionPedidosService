using AutoMapper;
using GestionPedidosService.Business.Extension;
using GestionPedidosService.Business.Handlers;
using GestionPedidosService.Business.ServicesQuery.Interfaces;
using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Domain.Models;
using GestionPedidosService.Domain.Utils;
using GestionPedidosService.Persistence.Handlers;
using GestionPedidosService.Persistence.Interfaces;
using GestionPedidosService.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static GestionPedidosService.Domain.Utils.ErrorsUtil;

namespace GestionPedidosService.Business.ServicesQuery.Implements
{
    public class OrderServiceQuery : IOrderServiceQuery
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;

        public OrderServiceQuery(IMapper mapper, IUnitOfWork uof)
        {
            _mapper = mapper;
            _uof = uof;
        }

        /*public async Task<PagedList<OrderRead>> GetAll(OrderQuery query)
        {
            var orderEntities = await _uof.orderDetailRepository.GetAll();
            var sortedByDate = orderEntities.OrderBy(e => e.Order.OrderDate).ToList();
            var filtered = ApplyFilters(sortedByDate, query);

            var orders = _mapper.Map<ICollection<OrderRead>>(filtered);
            return orders.ToPagedList(query.PageNumber, query.PageSize);
        }*/

        public async Task<PagedList<OrderRead>> GetAllByQuery(OrderQuery query)
        {
            try
            {
                var ordersDetails = await _uof.orderRepository.GetAllByQuery(query.AtelierId, query.OrderStatus, query.FilterString);
                var ordersRead = _mapper.Map<ICollection<OrderRead>>(ordersDetails);
                return ordersRead.ToPagedList(query.PageNumber, query.PageSize);
            }
            catch (RepositoryException ex)
            {
                throw new ServiceException(HttpStatusCode.InternalServerError, ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    HttpStatusCode.InternalServerError, 
                    ErrorsCode.GET_ORDERS_FAILED, 
                    ErrorMessages.GET_ORDERS_FAILED, 
                    ex);
            }
        }

        public async Task<OrderDetailRead> GetByCodeOrder_CodeGarment(string codeOrder, string codeGarment)
        {
            var orderDetail = await _uof.orderDetailRepository.GetByCodeOrder_CodeGarment(codeOrder, codeGarment);
            return null;
        }

        public async Task<OrderDetailRead> GetById(int id)
        {
            var order = await _uof.orderDetailRepository.GetById(id);
            return _mapper.Map<OrderDetailRead>(order);
        }

        /*private ICollection<OrderDetail> ApplyFilters(ICollection<OrderDetail> orders, OrderQuery query)
        {
            bool hasGarmentCode = query.CodeGarment != null;
            bool hasOrderStatus = query.OrderStatus != null;

            var filtered = orders.Where(e => !hasGarmentCode || e.Garment.CodeGarment.Contains(query.CodeGarment));

            if (hasOrderStatus)
            {
                EOrderStatus orderStatus = Enum.GetValues(typeof(EOrderStatus)).Cast<EOrderStatus>()
                    .FirstOrDefault(v => v.ToDescriptionString() == query.OrderStatus);

                filtered = filtered.Where(e => !hasOrderStatus || e.Order.OrderStatus.Equals(orderStatus));
            }

            return filtered.ToList();
        }*/
    }
}
