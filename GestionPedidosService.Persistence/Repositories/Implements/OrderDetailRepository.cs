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
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(AppDbContext context) : base(context) { }

        public override async Task<IEnumerable<OrderDetail>> GetAll()
        {
            return await _context.Set<OrderDetail>().Include(e => e.Garment).Include(e => e.Order).ToListAsync();
        }

        public async Task<OrderDetail> GetByCodeOrder_CodeGarment(string codeOrder, string codeGarment)
        {
            try
            {
                var orderDetail = await _context.OrderDetails
                .Include(od => od.Order).ThenInclude(o => o.UserClient)
                .Include(od => od.Order).ThenInclude(o => o.UserAtelier)
                .Include(od => od.Garment).ThenInclude(g => g.FeatureGarments)
                .AsSplitQuery()
                .FirstOrDefaultAsync(od => od.Order.CodeOrder.Equals(codeOrder) && od.Garment.CodeGarment.Equals(codeGarment));

                return orderDetail;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(
                    HttpStatusCode.InternalServerError,
                    ErrorsCode.GET_CONTEXT_ERROR,
                    ErrorMessages.GET_CONTEXT_ERROR,
                    ex);
            }
        }

        public override async Task<OrderDetail> GetById(int id)
        {
            return await _context.Set<OrderDetail>()
                .Include(e => e.Garment)
                .Include(e => e.Order)
                .Include(e => e.Garment.FeatureGarments)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}