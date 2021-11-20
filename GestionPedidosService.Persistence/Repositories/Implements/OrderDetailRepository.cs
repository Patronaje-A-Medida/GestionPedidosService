using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Interfaces;

namespace GestionPedidosService.Persistence.Repositories.Implements
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(AppDbContext context) : base(context) { }
    }
}