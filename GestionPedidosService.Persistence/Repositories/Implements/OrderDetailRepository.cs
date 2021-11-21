using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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