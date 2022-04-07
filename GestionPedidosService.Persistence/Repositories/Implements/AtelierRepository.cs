using GestionPedidosService.Domain.Entities;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GestionPedidosService.Persistence.Repositories.Implements
{
    public class AtelierRepository : Repository<Atelier>, IAtelierRepository
    {
        public AtelierRepository(AppDbContext context) : base(context) { }

        public async Task<Atelier> GetByGarmentId(int garmentId)
        {
            var atelier = await _context.Garments
                .Include(g => g.Atelier)
                .Where(g => g.Id == garmentId)
                .Select(g => g.Atelier)
                .FirstOrDefaultAsync();

            return atelier;
        }
    }
}
