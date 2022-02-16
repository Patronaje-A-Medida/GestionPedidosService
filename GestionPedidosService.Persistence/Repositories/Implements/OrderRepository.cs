using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Domain.Extensions;
using GestionPedidosService.Domain.Utils;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Handlers;
using GestionPedidosService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static GestionPedidosService.Domain.Utils.ErrorsUtil;

namespace GestionPedidosService.Persistence.Repositories.Implements
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetAllByQuery(int atelierId, string codeGarment, string orderStatus)
        {
            try
            {
                var status = Enum.GetValues(typeof(EOrderStatus))
                .Cast<EOrderStatus>()
                .FirstOrDefault(v => orderStatus == null || v.ToDescriptionString() == orderStatus);

                var orderDetails = await _context.Orders
                    .Include(o => o.OrderDetails).ThenInclude(od => od.Garment)
                    .Where(o => o.AtelierId == atelierId)
                    .Where(o => codeGarment == null || o.OrderDetails.Any(od => od.Garment.CodeGarment.ToUpper().Contains(codeGarment.ToUpper())))
                    .Where(o => orderStatus == null || o.OrderStatus.Equals(status))
                    .OrderBy(o => o.OrderDate)
                    .AsSplitQuery()
                    .ToListAsync();

                return orderDetails ?? new List<Order>();
            }
            catch(Exception ex)
            {
                throw new RepositoryException(
                    HttpStatusCode.InternalServerError, 
                    ErrorsCode.GET_CONTEXT_ERROR, 
                    ErrorMessages.GET_CONTEXT_ERROR, 
                    ex);
            }
        }
    }
}