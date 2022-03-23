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

        public async Task<IEnumerable<Order>> GetAllByQuery(int atelierId, int? orderStatus, string filterString)
        {
            try
            {
                /*var status = Enum.GetValues(typeof(EOrderStatus))
                .Cast<EOrderStatus>()
                .FirstOrDefault(v => orderStatus == null || v.ToDescriptionString() == orderStatus);*/

                var orderDetails = await _context.Orders
                    .AsNoTracking()
                    .Include(o => o.OrderDetails).ThenInclude(od => od.Garment)
                    .Include(o => o.UserClient).ThenInclude(u => u.User)
                    .Include(o => o.UserAtelier).ThenInclude(u => u.User)
                    .Where(o => o.AtelierId == atelierId)
                    .Where(
                        o => filterString == null || 
                        (
                            o.OrderDetails.Any(od => od.Garment.CodeGarment.ToUpper().Contains(filterString.ToUpper())) ||
                            o.CodeOrder.ToUpper().Contains(filterString.ToUpper()) ||
                            (o.UserClient.User.NameUser.ToUpper() + " " + o.UserClient.User.LastNameUser.ToUpper()).Contains(filterString.ToUpper())
                        )
                    )
                    .Where(o => orderStatus == null || o.OrderStatus.Equals((EOrderStatus)orderStatus))
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