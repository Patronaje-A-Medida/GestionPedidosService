using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Domain.Extensions;
using GestionPedidosService.Domain.Utils;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionPedidosService.Persistence.Repositories.Implements
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(AppDbContext context) : base(context) { }

        public override async Task<IEnumerable<OrderDetail>> GetAll()
        {
            return await _context.Set<OrderDetail>().Include(e => e.Garment).Include(e => e.Order).ToListAsync();
        }

        public async Task<IEnumerable<OrderDetail>> GetAllByQuery(int atelierId, string codeGarment, string orderStatus)
        {
            codeGarment = codeGarment.ToUpper();
            var eOrderStatus = Enum.GetValues(typeof(EOrderStatus))
                    .Cast<EOrderStatus>()
                    .FirstOrDefault(v => ( orderStatus == null || v.ToDescriptionString() == orderStatus));

            var orderDetails = await _context.Set<OrderDetail>()
                .Include(od => od.Garment)
                .Include(od => od.Order)//.ThenInclude(o => userclient)
                .Where(od => od.Order.AtelierId == atelierId)
                .Where(od => ( codeGarment == null || od.Garment.CodeGarment.ToUpper().Contains(codeGarment) ))
                .Where(od => ( orderStatus == null || od.Order.OrderStatus.Equals(eOrderStatus) ))
                .OrderBy(od => od.Order.OrderDate)
                .AsSplitQuery()
                .ToListAsync();

            return orderDetails ?? new List<OrderDetail>();
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