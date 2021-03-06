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

                var orders = await _context.Orders
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
                    .OrderByDescending(o => o.OrderDate)
                    .AsSplitQuery()
                    .ToListAsync();

                return orders ?? new List<Order>();
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

        public async Task<IEnumerable<Order>> GetByClientId(int userId)
        {
            try
            {
                var orders = await _context.Orders
                    .AsNoTracking()
                    .Include(o => o.OrderDetails).ThenInclude(od => od.Garment).ThenInclude(g => g.FeatureGarments)
                    .Include(o => o.UserClient).ThenInclude(u => u.User)
                    .Include(o => o.Atelier)
                    .Where(o => o.UserClientId == userId)
                    .OrderByDescending(o => o.OrderDate)
                    .AsSingleQuery()
                    .ToListAsync();

                return orders ?? new List<Order>();
            } catch (Exception ex)
            {
                throw new RepositoryException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.GET_ORDERS_CLIENT_FAILED,
                    ErrorMessages.GET_ORDERS_CLIENT_FAILED,
                    ex);
            }
        }

        public async Task<string> GetLastCodeOrderByAtelier()
        {
            var codeOrder = await _context.Orders
                .OrderByDescending(o => o.CodeOrder)
                .Select(o => o.CodeOrder)
                .FirstOrDefaultAsync();

            return codeOrder;
        }
    }
}