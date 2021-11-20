using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Interfaces;

namespace GestionPedidosService.Persistence.Repositories.Implements
{
    public class PatternDimensionRepository : Repository<PatternDimension>, IPatternDimensionRepository
    {
        public PatternDimensionRepository(AppDbContext context) : base(context) { }
    }
}