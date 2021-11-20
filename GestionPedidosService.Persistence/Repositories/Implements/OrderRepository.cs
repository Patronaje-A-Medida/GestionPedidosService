using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Interfaces;

namespace GestionPedidosService.Persistence.Repositories.Implements
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }
    }
}