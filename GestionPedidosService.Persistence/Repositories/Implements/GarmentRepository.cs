using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Interfaces;

namespace GestionPedidosService.Persistence.Repositories.Implements
{
    public class GarmentRepository : Repository<Garment>, IGarmentRepository
    {
        public GarmentRepository(AppDbContext context) : base(context) { }
    }
}