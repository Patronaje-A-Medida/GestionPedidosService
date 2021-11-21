using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Interfaces;

namespace GestionPedidosService.Persistence.Repositories.Implements
{
    public class PatternGarmentRepository : Repository<PatternGarment>, IPatternGarmentRepository
    {
        public PatternGarmentRepository(AppDbContext context) : base(context) { }
    }
}