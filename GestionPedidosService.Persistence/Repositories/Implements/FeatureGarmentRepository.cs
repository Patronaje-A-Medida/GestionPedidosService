using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Interfaces;

namespace GestionPedidosService.Persistence.Repositories.Implements
{
    public class FeatureGarmentRepository : Repository<FeatureGarment>, IFeatureGarmentRepository
    {
        public FeatureGarmentRepository(AppDbContext context) : base(context) { }
    }
}