using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionPedidosService.Persistence.Repositories.Implements
{
    public class PatternGarmentRepository : Repository<PatternGarment>, IPatternGarmentRepository
    {
        public PatternGarmentRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<PatternGarment>> GetAllByGarmentId(int id)
        {
            return await _context.Set<PatternGarment>().Include(e => e.PatternDimensions).Where(e => e.GarmentId == id).ToListAsync();
        }
    }
}